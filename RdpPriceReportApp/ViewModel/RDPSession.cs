using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RdpRealTimePricing.Events;
using LSEG.Data;
using LSEG.Data.Core;


namespace RdpPriceReportApp.ViewModel
{
    public class RdpSession
    {

        #region RtdsCredential
        public string RtdsUsername = "";
        public string RtdsAppid = "256";
        public string RtdsPosition = "localhost/net";
        public string WebSocketHost = "";
        #endregion

        #region DPUserCredential
        public string DPClientID = "";
        public string DPClientSecret = "";
        #endregion

        private ISession _session;
        internal ISession ServiceSession => _session ?? throw new Exception("Please call InitWebSocketConnectionAsync to initialize Data Platform Session ");
        //IStream stream;
        public Session.State SessionState { get; internal set; }
        public bool IsLoggedIn { get; set; }

        public RdpSession()
        {
            this.IsLoggedIn = false;
            this._session = null;
            SessionState = Session.State.Closed;
        }

        public void CloseSession()
        {
            OnStateChangedEvents = null;
            OnSessionEvents = null;
            _session.Close();
            SessionState = Session.State.Closed;
            
            _session = null;
        }
        public Task InitWebSocketConnectionAsync(bool useRdp)
        {
            return Task.Run(() =>
            {
                Log.Level = NLog.LogLevel.Off;

                if (!useRdp)
                {
                    _session = PlatformSession.Definition()
                       .Host(WebSocketHost)
                       .DacsUserName(RtdsUsername)
                       .DacsApplicationID(RtdsAppid)
                       .DacsPosition(RtdsPosition)
                       .GetSession()
                       .OnState(ProcessOnState)
                       .OnEvent(ProcessOnEvent);
                }
                else
                {
                    System.Console.WriteLine("Start PlatformSession");

                    _session = PlatformSession.Definition().OAuthGrantType(
                        new ClientCredentials().ClientID(DPClientID).ClientSecret(DPClientSecret))
                        .GetSession().OnEvent(ProcessOnEvent).OnState(ProcessOnState);
                }
                _session.OpenAsync().ConfigureAwait(false);

            });
        }
        private void ProcessOnEvent(Session.EventCode code, JObject message, ISession session)
        {
            RaiseSessionEvent(code, message);
        }
        private void ProcessOnState(Session.State state, string message, ISession session)
        {
            SessionState = state;
            RaiseStateChanged(state, message);
        }
        public event EventHandler<OnStateChangedEventArgs> OnStateChangedEvents;
        public event EventHandler<OnSessionEventArgs> OnSessionEvents;

        protected void RaiseStateChanged(Session.State state, string message)
        {
            var sessionState = new OnStateChangedEventArgs() { State = state, Message = message };
            OnStateChanged(sessionState);
        }
        protected virtual void OnStateChanged(OnStateChangedEventArgs e)
        {
            var handler = OnStateChangedEvents;
            handler?.Invoke(this, e);
        }
        protected void RaiseSessionEvent(Session.EventCode code, JObject message)
        {
            var connectionState = new OnSessionEventArgs() { EventCode = code, Message = message };
            OnSessionEvent(connectionState);
        }
        protected virtual void OnSessionEvent(OnSessionEventArgs e)
        {
            var handler = OnSessionEvents;
            handler?.Invoke(this, e);
        }
        
       
    }
}
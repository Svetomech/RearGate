using System;
using System.Management;
using System.Threading;

namespace RearGate
{
    public class GateGuard : IGuard
    {
        private static GateGuard instance;

        private ManagementEventWatcher guard;
        private WqlEventQuery duty;

        private GateVisitor visitor;

        private GateGuard()
        {
            guard = new ManagementEventWatcher();
            duty = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2 or EventType = 3");

            visitor = new GateVisitor();

            guard.EventArrived += (s, e) =>
            {
                visitor.Name = e.NewEvent.Properties["DriveName"].Value.ToString();
                visitor.State = (VisitorState)(Convert.ToInt16(e.NewEvent.Properties["EventType"].Value));
            };
            guard.Query = duty;
        }

        public static GateGuard Instance => instance ?? (instance = new GateGuard());

        public GateVisitor Visitor => visitor;

        public bool IsBusy { get; private set; }

        public void WakeUp()
        {
            guard.Start();

            IsBusy = true;
        }

        public void DoDuty()
        {
            guard.WaitForNextEvent();

            // Give EventArrived time to update visitor
            Thread.Sleep(1);
        }

        public void GoToBed()
        {
            guard.Stop();

            IsBusy = false;
        }
    }
}

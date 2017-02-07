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

        private GateGuard()
        {
            guard = new ManagementEventWatcher();
            duty = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2 or EventType = 3");

            Visitor = new GateVisitor();

            guard.EventArrived += (s, e) =>
            {
                Visitor.Name = e.NewEvent.Properties["DriveName"].Value.ToString();
                Visitor.State = (VisitorState) Convert.ToInt16(e.NewEvent.Properties["EventType"].Value);
            };
            guard.Query = duty;
        }

        public static GateGuard Instance => instance ?? (instance = new GateGuard());

        public GateVisitor Visitor { get; private set; }

        public bool IsBusy { get; private set; }

        public void WakeUp()
        {
            guard.Start();

            IsBusy = true;
        }

        public void DoDuty()
        {
            guard.WaitForNextEvent();

            Thread.Sleep(5); // give EventArrived time to update Visitor
        }

        public void GoToBed()
        {
            guard.Stop();

            IsBusy = false;
        }
    }
}

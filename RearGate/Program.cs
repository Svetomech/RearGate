using System;

namespace RearGate
{
    class Program
    {
        static void Main(string[] args)
        {
            var guard = GateGuard.Instance;
            var visitor = guard.Visitor;

            guard.WakeUp();

            while (guard.IsBusy)
            {
                guard.DoDuty();

                switch (visitor.State)
                {
                    case VisitorState.Entered:
                        Console.WriteLine($"Entered {visitor.Name}");
                        break;

                    case VisitorState.Left:
                        Console.WriteLine($"Left {visitor.Name}");
                        break;

                    default:
                        guard.GoToBed();
                        break;
                }
            }
        }
    }
}

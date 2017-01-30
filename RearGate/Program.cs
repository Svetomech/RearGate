using System;
using System.Diagnostics;
using System.IO;

namespace RearGate
{
    class Program
    {
        const string _authorizationKey = "RearVisitor.exe";

        static void Main()
        {
            var guard = GateGuard.Instance;
            var visitor = guard.Visitor;

            guard.WakeUp();

            while (guard.IsBusy)
            {
                guard.DoDuty();

                Console.WriteLine(String.Join(" ", visitor.Name, visitor.State));

                if (VisitorState.Entered == visitor.State)
                {
                    Authorize(ref visitor);
                }
            }
        }

        static void Authorize(ref GateVisitor visitor)
        {
            string keyLocation = Path.Combine(visitor.Name, _authorizationKey);

            if (!File.Exists(keyLocation))
            {
                return;
            }

            try
            {
                Process.Start(keyLocation);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

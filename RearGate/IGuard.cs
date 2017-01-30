namespace RearGate
{
    interface IGuard
    {
        GateVisitor Visitor { get; }

        bool IsBusy { get; }

        void WakeUp();

        void DoDuty();

        void GoToBed();
    }
}

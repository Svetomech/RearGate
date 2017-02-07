namespace RearGate
{
    interface IGuard
    {
        bool IsBusy { get; }

        void WakeUp();

        void DoDuty();

        void GoToBed();
    }
}

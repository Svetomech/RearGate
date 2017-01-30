namespace RearGate
{
    public enum VisitorState
    {
        Entered = 2,
        Left
    }

    interface IVisitor
    {
        string Name { get; set; }

        VisitorState State { get; set; }
    }
}

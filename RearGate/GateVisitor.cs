namespace RearGate
{
    public class GateVisitor : IVisitor
    {
        public string Name { get; set; }

        public VisitorState State { get; set; }
    }
}
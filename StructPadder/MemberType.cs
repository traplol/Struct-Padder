namespace StructPadder
{
    class MemberType
    {
        public string Name { get; set; }
        public virtual int Size { get; set; }

        public MemberType(string name)
        {
            Name = name;
        }
    }
}

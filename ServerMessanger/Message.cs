using System;

namespace ServerMessanger
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime WrittenDate { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Text { get; set; }         
    }
}

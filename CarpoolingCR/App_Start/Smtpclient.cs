namespace CarpoolingCR
{
    internal class Smtpclient
    {
        private object p1;
        private object p2;

        public Smtpclient(object p1, object p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        public object credentials { get; set; }
        public bool enablessl { get; set; }
    }
}
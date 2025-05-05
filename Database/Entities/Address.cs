namespace StudentMangementSystemC8.Database.Entities
{
    public class Address
    {
        public Guid? Id { get; set; }
        public required string Country { get; set; }
        public required string Province { get; set; }
        public required string City { get; set; }
        public required string Tole { get; set; }

        //Navigation Property
        //public Student? Student { get; set; }

    }
}

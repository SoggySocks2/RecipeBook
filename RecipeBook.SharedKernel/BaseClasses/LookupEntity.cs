namespace RecipeBook.SharedKernel.BaseClasses
{
    public abstract class LookupEntity
    {
        public int Id { get; protected set; }
        public string Description { get; protected set; }
        public bool IsDeleted { get; private set; }

        protected LookupEntity()
        {
        }

        public LookupEntity(string description)
        {
            SetDescription(description);
            IsDeleted = false;
        }

        public void MarkDeleted()
        {
            IsDeleted = true;
        }

        public void Update(string description)
        {
            SetDescription(description);
        }

        protected void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                description = string.Empty;
            }

            Description = description;
        }
    }
}

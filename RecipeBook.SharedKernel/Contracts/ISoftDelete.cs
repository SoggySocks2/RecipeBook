﻿namespace RecipeBook.SharedKernel.Contracts
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; }
    }
}

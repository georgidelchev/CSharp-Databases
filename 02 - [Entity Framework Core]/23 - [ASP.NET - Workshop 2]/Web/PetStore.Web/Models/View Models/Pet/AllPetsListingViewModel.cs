using System;
using System.Collections.Generic;
using PetStore.Services.Models.Pet;

namespace PetStore.Web.Models.View_Models.Pet
{
    public class AllPetsListingViewModel
    {
        public IEnumerable<PetListingServiceModel> Pets { get; set; }

        public int Total { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage
            => this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage + 1;

        public bool PreviousDisabled
            => this.CurrentPage == 1;

        public bool NextDisabled
        {
            get
            {
                var maxPage = Math.Ceiling(((double)this.Total) / 25);

                return maxPage == this.CurrentPage;
            }
        }
    }
}
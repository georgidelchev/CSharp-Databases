using System;
using System.Collections.Generic;
using System.Linq;

namespace CompositePattern
{
    public class CompositeGift : GiftBase, IGiftOperations
    {
        private readonly List<GiftBase> gifts;

        public CompositeGift(string name, int price)
            : base(name, price)
        {
            this.gifts = new List<GiftBase>();
        }

        public override int CalculateTotalPrice()
        {
            Console.WriteLine($"{this.name} constains the following products with prices:");

            return gifts.Sum(gift => gift.CalculateTotalPrice());
        }

        public void Add(GiftBase gift)
        {
            this.gifts.Add(gift);
        }

        public void Remove(GiftBase gift)
        {
            this.gifts.Remove(gift);
        }
    }
}
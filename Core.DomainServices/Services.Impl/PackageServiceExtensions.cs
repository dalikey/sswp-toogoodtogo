namespace Core.DomainServices.Services.Impl {
    public static class PackageServiceExtensions {
        public static void IsPickupDateTwoDayValid(this DateTime pickupDate) {
            var twoDaysLater = DateTime.Now.AddDays(2);
            if (pickupDate > twoDaysLater.Date)
                throw new ArgumentException("Ophaaldatum mag maximaal 2 dagen vooruit gepland worden");
            if (pickupDate < DateTime.Now.Date)
                throw new ArgumentException("Ophaaldatum kan niet in het verleden worden gemaakt");
        }

        public static void IsEndOfPickupTimeTwoDayValid(this DateTime endOfPickupTime) {
            var twoDaysLater = DateTime.Now.AddDays(2);
            if (endOfPickupTime > twoDaysLater.Date)
                throw new ArgumentException("Einde ophaaltijd mag maximaal 2 dagen vooruit gepland worden");
            if (endOfPickupTime < DateTime.Now.Date)
                throw new ArgumentException("Einde ophaaltijd kan niet in het verleden worden gemaakt");
        }
    }
}

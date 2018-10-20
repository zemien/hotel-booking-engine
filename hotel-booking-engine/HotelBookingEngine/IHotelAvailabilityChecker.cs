namespace HotelBookingEngine
{
    public interface IHotelAvailabilityChecker
    {
        /// <summary>
        /// Checks if this hotel is available to meet the booking request.
        /// </summary>
        /// <returns><c>true</c>, if available, <c>false</c> otherwise.</returns>
        /// <param name="bookingRequest">Booking request.</param>
        bool IsAvailable(BookingRequest bookingRequest);
    }

    /// <summary>
    /// Here's an example where it may check the database for availability,
    /// while other implementations may call a web service provided by the hotel chain.
    /// </summary>
    internal class DbHotelAvailabilityChecker : IHotelAvailabilityChecker
    {
        public DbHotelAvailabilityChecker(object database)
        {
            //Dependency on a database connection would be injected in this case
        }

        public bool IsAvailable(BookingRequest bookingRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}

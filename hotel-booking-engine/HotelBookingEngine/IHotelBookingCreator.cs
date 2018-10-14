namespace HotelBookingEngine
{
    public interface IHotelBookingCreator
    {
        /// <summary>
        /// Creates the booking based on a request.
        /// </summary>
        /// <returns>The result of the booking request.</returns>
        /// <param name="bookingRequest">Booking request.</param>
        BookingRequestResult CreateBooking(BookingRequest bookingRequest);
    }
}

using System;

namespace HotelBookingEngine
{
    public class HotelBookingCreator : IHotelBookingCreator
    {
        public BookingRequestResult CreateBooking(BookingRequest bookingRequest)
        {
            if (bookingRequest == null)
            {
                return new BookingRequestResult(null, "No request");
            }

            if (bookingRequest.Hotel == null)
            {
                return new BookingRequestResult(null, "No hotel");
            }

            throw new NotImplementedException();
        }
    }
}

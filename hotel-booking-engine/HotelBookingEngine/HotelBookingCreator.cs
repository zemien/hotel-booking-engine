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

            if (bookingRequest.RoomQuantity <= 0)
            {
                return new BookingRequestResult(null, "Invalid room quantity");
            }

            if (bookingRequest.CheckInLocalDate >= bookingRequest.CheckOutLocalDate)
            {
                return new BookingRequestResult(null, "Invalid dates");
            }

            throw new NotImplementedException();
        }
    }
}

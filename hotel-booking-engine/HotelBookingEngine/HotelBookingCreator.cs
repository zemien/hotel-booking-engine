using System;

namespace HotelBookingEngine
{
    public class HotelBookingCreator : IHotelBookingCreator
    {
        public BookingRequestResult CreateBooking(BookingRequest bookingRequest)
        {
            //In the real world, this might produce an error object instead
            var errorMessage = ValidateBooking(bookingRequest);

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                return new BookingRequestResult(null, errorMessage);
            }

            throw new NotImplementedException();
        }

        private string ValidateBooking(BookingRequest bookingRequest)
        {
            if (bookingRequest == null)
            {
                return "No request";
            }

            if (bookingRequest.Hotel == null)
            {
                return "No hotel";
            }

            if (bookingRequest.RoomQuantity <= 0)
            {
                return "Invalid room quantity";
            }

            if (bookingRequest.CheckInLocalDate >= bookingRequest.CheckOutLocalDate)
            {
                return "Invalid dates";
            }

            return null;
        }
    }
}

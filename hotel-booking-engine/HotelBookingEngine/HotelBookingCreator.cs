using System;

namespace HotelBookingEngine
{
    public class HotelBookingCreator : IHotelBookingCreator
    {
        private readonly IHotelAvailabilityChecker _hotelAvailabilityChecker;

        public HotelBookingCreator(IHotelAvailabilityChecker hotelAvailabilityChecker)
        {
            _hotelAvailabilityChecker = hotelAvailabilityChecker;
        }

        public BookingRequestResult CreateBooking(BookingRequest bookingRequest)
        {
            //In the real world, this might produce an error object instead
            var errorMessage = ValidateBooking(bookingRequest);

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                return new BookingRequestResult(null, errorMessage);
            }

            //Prepare result package
            var confirmedBooking = new Booking(bookingRequest);
            return new BookingRequestResult(confirmedBooking, null);
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

            var createdLocalDate = ConvertToLocalDate(bookingRequest.CreatedUtcDateTime, bookingRequest.Hotel.TimeZone);
            if (bookingRequest.CheckInLocalDate < createdLocalDate)
            {
                return "Invalid dates";
            }

            if (!_hotelAvailabilityChecker.IsAvailable(bookingRequest))
            {
                return "No availability";
            }

            return null;
        }

        private DateTime ConvertToLocalDate(DateTime utcDateTime, string localTimeZone)
        {
            //In the real world, you would use a library like NodaTime
            //to perform the conversion for you. I'm not doing any conversions
            //here because this is a demonstration of concepts.
            return utcDateTime.Date;
        }
    }
}

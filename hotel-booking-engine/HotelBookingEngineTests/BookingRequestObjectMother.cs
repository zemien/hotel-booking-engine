using System;
using HotelBookingEngine;

namespace HotelBookingEngineTests
{
    /// <summary>
    /// Object mother pattern for creating test data.
    /// Recommended to set it to internal so that it's not
    /// inadvertently used in production code.
    /// </summary>
    internal class BookingRequestObjectMother
    {
        public static BookingRequest CreateValidBookingRequest()
        {
            var hotel = new Hotel();
            var bookingRequest = new BookingRequest(hotel, 5,
                new DateTime(2018, 10, 30), new DateTime(2018, 11, 07),
                new DateTime(2018, 05, 05).ToUniversalTime());
            return bookingRequest;
        }
    }
}

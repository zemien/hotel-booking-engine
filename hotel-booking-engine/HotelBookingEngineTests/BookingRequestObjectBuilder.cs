using System;
using HotelBookingEngine;

namespace HotelBookingEngineTests
{
    /// <summary>
    /// Booking request object builder for building test data.
    /// Recommended to set it to internal so that it's not
    /// inadvertently used in production code.
    /// </summary>
    internal class BookingRequestObjectBuilder
    {
        private string _hotelName;
        private double _publishedRoomRate;
        private int _roomQuantity;
        private double _discount;
        private DateTime _checkInLocalDate;
        private DateTime _checkOutLocalDate;
        private DateTime _createdUtcDateTime;

        public BookingRequestObjectBuilder()
        {
            //Set up defaults for a valid object
            _hotelName = "Tongariro Lodge";
            _publishedRoomRate = 200;
            _roomQuantity = 5;
            _discount = 0;
            _checkInLocalDate = new DateTime(2018, 10, 30);
            _checkOutLocalDate = new DateTime(2018, 11, 07);
            _createdUtcDateTime = new DateTime(2018, 05, 05).ToUniversalTime();
        }

        public BookingRequestObjectBuilder SetHotelName(string hotelName)
        {
            _hotelName = hotelName;
            return this;
        }

        public BookingRequestObjectBuilder SetPublishedRoomRate(double publishedRoomRate)
        {
            _publishedRoomRate = publishedRoomRate;
            return this;
        }

        public BookingRequestObjectBuilder SetRoomQuantity(int roomQuantity)
        {
            _roomQuantity = roomQuantity;
            return this;
        }

        public BookingRequestObjectBuilder SetDiscount(double discount)
        {
            _discount = discount;
            return this;
        }

        public BookingRequestObjectBuilder SetCheckInLocalDate(DateTime checkInLocalDate)
        {
            _checkInLocalDate = checkInLocalDate;
            return this;
        }

        public BookingRequestObjectBuilder SetCheckOutLocalDate(DateTime checkOutLocalDate)
        {
            _checkOutLocalDate = checkOutLocalDate;
            return this;
        }

        public BookingRequestObjectBuilder SetCreatedUtcDateTime(DateTime createdUtcDateTime)
        {
            _createdUtcDateTime = createdUtcDateTime;
            return this;
        }

        public BookingRequest Build()
        {
            var hotel = new Hotel(_hotelName, _publishedRoomRate);
            var request = new BookingRequest(hotel, _roomQuantity, 
                _checkInLocalDate, _checkOutLocalDate, _createdUtcDateTime,
                _discount);
            return request;
        }
    }
}

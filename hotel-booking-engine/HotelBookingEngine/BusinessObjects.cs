/*
 * I would normally break these up into individual files,
 * but have kept them together for demo purposes.
 * 
 * I have added comments to only non-obvious properties
 * for the same reasons.
 */
using System;

namespace HotelBookingEngine
{
    public class Hotel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }

        /// <summary>
        /// Gets the time zone that this hotel is in.
        /// In the real world this might be the time zone id
        /// used by NodaTime, i.e. "Pacific/Auckland" for NZST/NZDT
        /// </summary>
        /// <value>The time zone.</value>
        public string TimeZone { get; private set; }

        /// <summary>
        /// Gets the nice-sounding description of the room.
        /// In the real world, there may be different room types.
        /// </summary>
        /// <value>The room description.</value>
        public string RoomDescription { get; private set; }

        /// <summary>
        /// Gets the published room rate, which is the standard
        /// rate of the room before any promotions or discounts.
        /// In the real world, the published room rate will vary
        /// with seasonal demands.
        /// </summary>
        /// <value>The published room rate.</value>
        public double PublishedRoomRate { get; private set; }
    }

    public class Booking
    {
        public Booking(BookingRequest bookingRequest)
        {
            BookingRequest = bookingRequest;
        }

        public int Id { get; private set; }
        public Hotel Hotel { get; private set; }

        /// <summary>
        /// Gets the original booking request that created this booking
        /// </summary>
        /// <value>The booking request.</value>
        public BookingRequest BookingRequest { get; private set; }

        /// <summary>
        /// Gets the number of rooms booked.
        /// In the real world this may vary day by day.
        /// </summary>
        /// <value>The room quantity.</value>
        public int RoomQuantity { get; private set; }

        /// <summary>
        /// Gets the check in date in the hotel's timezone.
        /// In the real world, it is best to use a date-only structure
        /// from a library such as NodaTime to avoid confusion with 
        /// the time aspect of a DateTime.
        /// </summary>
        /// <value>The check in local date.</value>
        public DateTime CheckInLocalDate { get; private set; }
        public DateTime CheckOutLocalDate { get; private set; }

        /// <summary>
        /// Gets the person's name for the booking.
        /// </summary>
        /// <value>The name of the booking.</value>
        public string BookingName { get; private set; }
        public string BookingAddress { get; private set; }
        public string BookingPhone { get; private set; }
        public string BookingEmail { get; private set; }

        /// <summary>
        /// Gets the confirmed room rate, after discounts.
        /// In the real world, there may be fees and taxes added on top of this.
        /// </summary>
        /// <value>The booking room rate.</value>
        public double BookingRoomRate { get; private set; }

        /// <summary>
        /// Gets the date and time this booking was created, in UTC.
        /// In the real world, a booking version number will also be
        /// required to keep track of booking changes over time.
        /// </summary>
        /// <value>The created UTC date time.</value>
        public DateTime CreatedUtcDateTime { get; private set; }
    }

    /// <summary>
    /// Booking request, which represents a request to book a hotel.
    /// </summary>
    public class BookingRequest
    {
        public BookingRequest(Hotel hotel, int roomQuantity, DateTime checkInLocalDate,
            DateTime checkOutLocalDate, DateTime createdUtcDateTime)
        {
            Hotel = hotel;
            RoomQuantity = roomQuantity;
            CheckInLocalDate = checkInLocalDate;
            CheckOutLocalDate = checkOutLocalDate;
            CreatedUtcDateTime = createdUtcDateTime;
        }

        public int Id { get; private set; }
        public Hotel Hotel { get; private set; }
        public int RoomQuantity { get; private set; }
        public DateTime CheckInLocalDate { get; private set; }
        public DateTime CheckOutLocalDate { get; private set; }
        public string BookingName { get; private set; }
        public string BookingAddress { get; private set; }
        public string BookingPhone { get; private set; }
        public string BookingEmail { get; private set; }

        /// <summary>
        /// Gets the voucher code that was submitted.
        /// It is assumed the voucher code has not been validated
        /// when this request was created.
        /// </summary>
        /// <value>The voucher code.</value>
        public string VoucherCode { get; private set; }

        /// <summary>
        /// Gets the discount (in percentage, e.g. 0.15 for 15%)
        /// that will be applied to the <see cref="Hotel.PublishedRoomRate"/>.
        /// </summary>
        /// <value>The discount.</value>
        public double Discount { get; private set; }

        public DateTime CreatedUtcDateTime { get; private set; }
    }

    /// <summary>
    /// Booking request result, which will either have 
    /// a confirmed booking or an error message.
    /// </summary>
    public class BookingRequestResult
    {
        public BookingRequestResult(Booking confirmedBooking, string errorMessage)
        {
            ConfirmedBooking = confirmedBooking;
            ErrorMessage = errorMessage;
        }

        public Booking ConfirmedBooking { get; private set; }

        /// <summary>
        /// Gets the error message if the booking cannot be confirmed.
        /// In the real world, this might be a dedicated BookingRequestError
        /// object with an error code and detailed error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; private set; }
    }
}

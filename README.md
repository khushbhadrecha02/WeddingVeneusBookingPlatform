Wedding Venue Booking Platform
Developed a Wedding Venue Booking Platform using ASP.NET Framework where users can easily navigate through available venues based on applied filters like State, City, Rent Per Day. Users can check the status of venues by providing the date to see if a venue has been booked or not. If the venue is available on the selected date, users can book it, and the total rent will be calculated by the platform. Once the payment is successful, an invoice will be generated in the form of a PDF and sent to the user's email.
The platform also includes functionality for canceling venue bookings. When a user wants to cancel a booking, a request is sent to the venue owner. The request will be further processed only after the owner approves it, and the refund amount will also be calculated by the platform.
Additionally, robust authentication and role-based authorization have been implemented to ensure the system remains secure. For database connectivity, ADO.NET was used. The DinkToPdf library was utilized for generating invoices in PDF format, and the MailKit library was used for sending emails.
Users can also update their profile information such as password, email, and profile photo. Moreover, the platform includes a feature to add venue details to a favorite list.

Features
Venue Filtering: Filter venues based on State, City, Rent Per Day.
Availability Check: Check the availability of venues for a specific date.
Booking System: Book venues and calculate total rent automatically.
Invoice Generation: Generate invoices in PDF format and send them via email.
Booking Cancellation: Request venue booking cancellation with approval workflow and refund calculation.
Authentication & Authorization: Robust authentication and role-based authorization.
Profile Management: Update user profile details including password, email, and profile photo.
Favorite List: Add venue details to a favorite list.

Technologies Used
ASP.NET Framework,
ADO.NET (Database connectivity),
DinkToPdf (PDF generation),
MailKit (Email sending),
Bootstrap,
jQuery.

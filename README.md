# 🎟️ Event Booking System

A full-featured **Event Booking System** designed to make discovering, managing, and booking events simple and efficient.  
The system supports event browsing, ticket reservations, user management, reviews, wishlists, and administrative tools for event organizers and admins.  
Built with a modular clean architecture, the project ensures scalability, maintainability, and smooth operation across all components.




## 👥 User Roles & Permissions

| **User Type**       | **CAN Do**                                                                 | **CANNOT Do**                                      |
|---------------------|----------------------------------------------------------------------------|----------------------------------------------------|
| **Visitor**         | - Browse events<br>- Search & filter events<br>- View event details        | - Book tickets<br>- Leave reviews<br>- Save favorites |
| **Registered User** | - All visitor capabilities<br>- Book tickets<br>- Write reviews & ratings<br>- Manage profile<br>- Save favorites to wishlist | - Edit event details<br>- Access admin features |
| **Event Organizer** | - Create/edit own events<br>- Manage ticket types<br>- View booking statistics | - Modify other organizers' events |
| **Admin**           | - Full system access<br>- Manage all events<br>- Manage users<br>- View all reports | *None* |


## 🚀 Features

###  Authentication & Users
- Register & Login using JWT Authentication
- User roles (Admin / User)
- Manage user profile
- Secure authorization for protected endpoints

###  Event Management
- Create, update, and delete events (Admin)
- Categorize events by category and status
- Multiple ticket types
- Search & filter events

###  Booking System
- Book tickets with availability check
- View user bookings
- Update or cancel booking
- Booking status (Pending, Confirmed, Cancelled)

###  Reviews
- Add reviews to events
- Edit / remove reviews
- Event rating system

###  Wishlist
- Add events to wishlist
- Remove from wishlist
- Retrieve all wishlist items

---

## 🏛️ Clean Architecture Overview

EventBookingSystem/
│
├── EventBooking.API/ → Presentation Layer
├── EventBooking.Application/ → Application Layer (Business Logic)
├── EventBooking.Domain/ → Domain Models & Enums
└── EventBooking.Infrastructure/ → Data Access, EF Core, Migrations
 
 
 ## 📂 Project Structure
 ```text
  EventBookingSystem/
│
├── 📁 EventBooking.API/                    # Presentation Layer
│   ├── 📁 Controllers/                    # API Controllers
│   │   ├── 📄 EventsController.cs
│   │   ├── 📄 BookingsController.cs
│   │   ├── 📄 AuthController.cs
│   │   ├── 📄 UsersController.cs
│   │   └── 📄 ReviewsController.cs
│   │
│   ├── 📄 Program.cs                      # Startup Configuration
│   └── 📄 appsettings.json                # Configuration
│
├── 📁 EventBooking.Application/           # Application Layer
│   ├── 📁 DTOs/                           # Data Transfer Objects
│   │   ├── 📁 Requests/                   # Request DTOs
│   │   │   ├── 📄 CreateEventRequest.cs
│   │   │   ├── 📄 UpdateEventRequest.cs
│   │   │   ├── 📄 BookTicketsRequest.cs
│   │   │   ├── 📄 RegisterRequest.cs
│   │   │   └── 📄 LoginRequest.cs
│   │   │
│   │   ├── 📁 Responses/                  # Response DTOs
│   │   │   ├── 📄 EventDto.cs
│   │   │   ├── 📄 BookingDto.cs
│   │   │   ├── 📄 UserDto.cs
│   │   │   └── 📄 AuthResponse.cs
│   │   │
│   │
│   ├── 📁 Interfaces/                     # Application Services Interfaces
│   │   ├── 📄 IEventService.cs
│   │   ├── 📄 IBookingService.cs
│   │   ├── 📄 IAuthService.cs
│   │   ├── 📄 IUserService.cs
│   │   └── 📄 IReviewService.cs
│   │
│   ├── 📁 Services/                       # Application Services
│   │   ├── 📄 EventService.cs
│   │   ├── 📄 BookingService.cs
│   │   ├── 📄 AuthService.cs
│   │   ├── 📄 UserService.cs
│   │   └── 📄 ReviewService.cs
│   │
│   │
│   │
│
├── 📁 EventBooking.Domain/                # Domain Layer
│   ├── 📁 Entities/                       # Domain Entities
│   │   ├── 📄 User.cs
│   │   ├── 📄 Event.cs
│   │   ├── 📄 Booking.cs
│   │   ├── 📄 TicketType.cs
│   │   ├── 📄 Review.cs
│   │   ├── 📄 Wishlist.cs
│   │   
│   │
│   │
│   ├── 📁 Enums/                          # Enumerations
│   │   ├── 📄 UserRole.cs
│   │   ├── 📄 EventCategory.cs
│   │   ├── 📄 BookingStatus.cs
│   │   └── 📄 EventStatus.cs
│   │
│
├── 📁 EventBooking.Infrastructure/        # Infrastructure Layer
│   ├── 📁 Data/                           # Data Access
│   │   ├── 📄 ApplicationDbContext.cs
│   │   │
│   │   │
│   │   ├── 📁 Migrations/                 # EF Migrations
│   │   │   └── 📄 [Timestamp]_InitialCreate.cs
│   │   │
│   │   └── 📁 Seeds/                    
           └── 📄 DatabaseSeeder.cs
```
## 🛠️ Technologies Used

| Technology | Description |
|-----------|-------------|
| ASP.NET Core 8 Web API | Backend framework |
| Entity Framework Core | ORM |
| SQL Server | Database |
| AutoMapper | Object mapping |
| JWT Authentication | Secure authentication |
| Clean Architecture | Project structure |
---

## 📘 Example Endpoints

## 🔐 Authentication

| **Method** | **Endpoint**              | **Description**             |
|------------|---------------------------|-----------------------------|
| POST       | `/api/auth/register`      | Register a new user         |
| POST       | `/api/auth/login`         | Login and receive JWT token |

## 👤 Users

| **Method** | **Endpoint**                        | **Description**                 |
|------------|-------------------------------------|---------------------------------|
| GET        | `/api/users/me`                     | Get current user profile        |
| PUT        | `/api/users/me`                     | Update current user profile     |
| GET        | `/api/users/me/bookings`            | Get my bookings                 |
| GET        | `/api/users/me/wishlist`            | Get my wishlist                 |
| POST       | `/api/users/me/wishlist/{eventId}`  | Add event to wishlist           |
| DELETE     | `/api/users/me/wishlist/{eventId}`  | Remove event from wishlist      |

## 🎤 Events

| **Method** | **Endpoint**                  | **Description**                 |
|------------|-------------------------------|---------------------------------|
| GET        | `/api/events`                 | Get all events                  |
| GET        | `/api/events/{id}`            | Get event by ID                 |
| GET        | `/api/events/{id}/tickets`    | Get event tickets               |

## 🎫 Bookings

| **Method** | **Endpoint**                        | **Description**               |
|------------|-------------------------------------|-------------------------------|
| POST       | `/api/bookings`                     | Create a new booking          |
| GET        | `/api/bookings/me`                  | Get my bookings               |

## ⭐ Reviews

| **Method** | **Endpoint**                        | **Description**               |
|------------|-------------------------------------|-------------------------------|
| POST       | `/api/reviews/{eventId}`            | Add review for event          |
| GET        | `/api/reviews/events/{eventId}`     | Get event reviews             |
| DELETE     | `/api/reviews/{id}`                 | Delete review                 |

## 🛠️ Admin Endpoints

### 🎤 Events Management

| **Method** | **Endpoint**                    | **Description**         |
|------------|---------------------------------|-------------------------|
| POST       | `/api/admin/events`             | Create event            |
| PUT        | `/api/admin/events/{id}`        | Update event            |
| DELETE     | `/api/admin/events/{id}`        | Delete event            |
| POST       | `/api/admin/events/{id}/tickets`| Add ticket type         |

### 🎟️ Ticket Types Management

| **Method** | **Endpoint**                    | **Description**         |
|------------|---------------------------------|-------------------------|
| PUT        | `/api/admin/tickets/{id}`       | Update ticket           |
| DELETE     | `/api/admin/tickets/{id}`       | Delete ticket           |

### 📦 Bookings Management

| **Method** | **Endpoint**                    | **Description**         |
|------------|---------------------------------|-------------------------|
| GET        | `/api/admin/bookings`           | Get all bookings        |
| PUT        | `/api/admin/bookings/{id}/status` | Update booking status   |
---

## 📡 API Screenshots

<img width="1600" height="557" alt="image" src="https://github.com/user-attachments/assets/2f8d8a77-d879-40b3-8261-6ea407bc8529" />
<img width="1600" height="623" alt="image" src="https://github.com/user-attachments/assets/635e8ddb-18ca-4207-9dd5-e01140b95784" />
<img width="1600" height="718" alt="image" src="https://github.com/user-attachments/assets/9b0ca77c-3fc7-4007-813b-1d662be1b9ca" />

## 🔄 Application Flow

### 👤 User Journey
1. **Auth**: Register or Login to get a JWT token
2. **Discovery**: Browse events and view ticket types
3. **Transaction**: Create a booking for a specific event
4. **Interaction**: Manage wishlist and leave reviews after attending

### 🛠️ Admin Journey
1. **Management**: Create/Update events and manage ticket inventory
2. **Assets**: Upload event images
3. **Operations**: Monitor all bookings and update statuses (Confirmed/Pending)

## 🖼️ Website Screenshots

<img width="1905" height="865" alt="image" src="https://github.com/user-attachments/assets/ffc4bbe3-52e8-4d70-afe6-4e89cbd6c2b2" />
<img width="1900" height="862" alt="image" src="https://github.com/user-attachments/assets/23484bca-3c8b-45f9-bd28-05e05ea5acba" />
<img width="1887" height="866" alt="image" src="https://github.com/user-attachments/assets/3bd3343b-24d1-47d4-9436-79c938300e48" />
<img width="1867" height="867" alt="image" src="https://github.com/user-attachments/assets/4cddb474-8a36-47a2-95b1-85a75457eafa" />
<img width="1830" height="872" alt="image" src="https://github.com/user-attachments/assets/0ff6ad50-9d27-468c-8bc2-78b3454b4294" />

---
📊 Diagrams

## Class Diagram

<img width="1492" height="792" alt="image" src="https://github.com/user-attachments/assets/3fe1beab-2912-49b9-bcbe-8d350727ed54" />

## sequenceDiagram
```mermaid
sequenceDiagram
    actor U as User
    participant A as API
    participant D as Database

    U->>A: Register/Login
    A->>D: Save/Check user
    D-->>A: User data
    A-->>U: Token
    
    U->>A: View events
    A->>D: Get events
    D-->>A: Events
    A-->>U: Show events
    
    U->>A: Book event
    A->>D: Check & save booking
    D-->>A: Booking saved
    A-->>U: Booking confirmed
    
    U->>A: Add review
    A->>D: Save review
    D-->>A: Review saved
    A-->>U: Review added
    
    actor Admin
    Admin->>A: Manage events
    A->>D: Update events
    D-->>A: Updated
    A-->>Admin: Done

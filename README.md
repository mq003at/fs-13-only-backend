# Acacia Ecommerce - Backend
![Generic badge](https://img.shields.io/badge/.NET-Core6-purple.svg)
![Generic badge](https://img.shields.io/badge/EntityFrameworkCore-v.6.0.15-red.svg)
![Generic badge](https://img.shields.io/badge/IdentityModal-v.6.28.1-pink.svg)
![Generic badge](https://img.shields.io/badge/Swagger-v.6.5.0-green.svg)
![Generic badge](https://img.shields.io/badge/Json-v.13.0-black.svg)

# Testing
Testing directory is located in backend-testing. You can run the following command line to start the testing process in that directory.

` dotnet test --logger "console;verbosity=detailed"`

# Project Structure
<details>
<summary>Open Project Structure</summary>


```bash
└── frontend
    ├── assets
    │   ├── fonts
    │   └── images.png
    ├── components
    │   ├── basic
    │   │   ├── Error.tsx
    │   │   ├── ProductCard.tsx
    │   │   └── SaleIcon.tsx
    │   ├── cart
    │   │   └── Cart.tsx
    │   ├── frontPage
    │   │   ├── FrontPage.tsx
    │   │   └── SpecialOffers.tsx
    │   ├── functions
    │   │   └── common.tsx
    │   ├── header
    │   │   ├── Banner.tsx
    │   │   ├── Header.tsx
    │   │   ├── HeaderButtons.tsx
    │   │   ├── LeftNav.tsx
    │   │   ├── MiddleNav.tsx
    │   │   └── RightNav.tsx
    │   ├── products
    │   │   ├── AddProductModal.tsx
    │   │   ├── CartItemDetails.tsx
    │   │   ├── ProductBox.tsx
    │   │   ├── ProductDetail.tsx
    │   │   ├── ProductFullDetails.tsx
    │   │   └── ProductList.tsx
    │   ├── profile
    │   │   ├── LogUser.tsx
    │   │   ├── Profile.tsx
    │   │   ├── ProfileSchema.tsx
    │   │   └── UserDetails.tsx
    │   ├── hooks
    │   │   └── reduxHook.ts
    │   ├── redux
    │   │   ├── reducers
    │   │   │   ├── cartReducer.ts
    │   │   │   ├── categoryReducer.ts
    │   │   │   ├── productReducer.ts
    │   │   │   └── userReducer.ts
    │   │   └── store.ts
    │   ├── styles
    │   │   ├── css
    │   │   ├── mui
    │   │   └── index.scss
    │   └── types
    │       ├── common.tsx
    │       ├── props.tsx
    │       └── user.tsx
    ├── App.tsx
    └── index.tsx
```
</details>

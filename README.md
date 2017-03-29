# Brandviser
## Trading platform for creative business names
[![Build status](https://ci.appveyor.com/api/projects/status/ifyo8nvkpvqdx7om?svg=true)](https://ci.appveyor.com/project/martinst1/brandviser)
[![Coverage Status](https://coveralls.io/repos/github/martinst1/Brandviser/badge.svg?branch=master)](https://coveralls.io/github/martinst1/Brandviser?branch=master)

## Idea
- Any seller can submit a .com domain name for approval
- Site admin approves or rejects the domain name
- Designer proposes logo 
- Site admin approves or rejects the logo
- Domain is posted and buyers can browse and buy

## Anonymous user
- Can browse home page with latest domains
- Can use the search page

## Seller
- Can be registered
- Has dashboard & stats
- Can add domain
- Can view pending domains
- Can view rejected domains
- Can view accepted domains 
  - Can validate ownership via nameserver change or txt records creation
- Can view pending design domains
- Can view published domains 
  - Can edit published domain description and price
- Can view sold domains

## Buyer
- Can be registered
- Has dashboard & stats
- Can view bought domains
- Can add funds to account
- Can buy domains

## Designer
- Can be registered
- Has dashboard & stats
- Can view approved domains waiting for logo design
- Can propose logo

## Admin
- Cannot be registered
- Has dashboard & stats
- Can view pending approval domains
  - Can approve or reject domain and set recommended price
- Can view pending logos
  - Can approve or reject logos

# Technologies
- ASP.NET MVC 5
- MSSQL SERVER 
- Entity Framework
- AppVeyor & coveralls
- Bytes2You Validation
- Ninject
- Moq, Nunit & TestStack FluentMVCTesting


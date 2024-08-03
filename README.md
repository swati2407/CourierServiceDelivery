# CourierServiceDelivery
CourierServiceDelivery

## Description
A c# console application which is used to estimated the package delivery price and estimated time the package will reach to the destination.

## Features
- Calculate the package delivery price based on the available offers
- Calculate the package delivery time based on it's weight and distance with the vehicle speed

## Installation
## Prerequisites
- .NET SDK

## Steps to Install
1. Clone the repository from the url https://github.com/swati2407/CourierServiceDelivery
2. Build the application by ensuring 
3. Navigate to the path Path_Where_You_Clone_The_Application\bin\Release\net8.0
4. CourierServiceDeliveryCostEstimation application should be availabel for use

## Usage

## How to run the Program
double click on CourierServiceDeliveryCostEstimation application, it will open in command prompt and will be ready for use.

## Input format
1. Enter the base delivery cost of package
2. Enter the number of packages
3. Enter the package details by using space separator(i.e. PKGID PKHWeight Distance OfferCode)
4. Enter Vehicle Details by using space separator(i.e. NoOfVehicles MaxSpeed MaxWeight)

## Input Example
Enter the base delivery cost
100
Enter the numbers of packages
3
Enter the package details by using space separator(i.e. PKGID PKHWeight Distance OfferCode)
PKG1 5 5 OFR001
PKG2 15 5 OFR002
PKG3 10 100 OFR003
Enter Vehicle Details by using space separator(i.e. NoOfVehicles MaxSpeed MaxWeight)
2 70 200

## Sample Output format
PkgId    discount    totalCost
PKG1   0   175
PKG2   0   275
PKG3   35   665

##Configuration
We have an offerCode.json file that has all the currently available offers. This file can be updated shortly for any new offers introduced and existing offers can be removed as well.

Currently, the offercode.json file contains the offer code, Package minimum and maximum weight, Package minimum and maximum distance, and the discount percentage. New fields in the future can also be accommodated in the same JSON file with extended implementation.

##Contact Information
Author: Swati, phone: 9453142367




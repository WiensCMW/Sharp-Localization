# Sharp-Localization
c# class that allows you to localize strings in your app, the localization data is stored on an embedded csv file.

## About the CSV Data file
The csv data structure is simple, the fist column is your apps native language and any additional language you wish to support should show up in columns B, C and etc.

The column headers should contain the c# `CultureInfo.Name` as the header. `en-CA` for English (Canada), `es-MX` for Spanish (Mexico) and etc.

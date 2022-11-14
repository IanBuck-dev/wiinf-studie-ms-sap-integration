# Study - Integration of 3rd party BI tool into Microsoft Power Apps

This repo is part of a feasibility study which explores the integration of third party applications, such as a business intelligence tool for detection double payments for invoices, into the Microsoft ecosystem.

In this study, the focus will be on the integration with PowerApps.

## API - Simple Backend for double payment candidates

The main part of this repo is the API, a small backend with C# and .NET, that offers multiple endpoints to fetch double payment candidates and set their judgement status.
There is no endpoint yet to import double payment candidates given SAP login information, we simply wrote a little script that seeds the included SQLite database for demo purposes.

Open todos include:
- Adding pagination (in case you have many entities)(easily implemented using Skip & Take)
- Adding authorization (f.e. easily implemented via API keys)

The API can be started locally via a tool such as [ngrok](https://ngrok.com/) to expose a online reachable url that can be used in the demo app.

```
ngrok http 80
```

## PowerApps - Demo App (todo)

Unfortunately, using git with PowerApps is only available as a experimental feature at the moment: [Use Git version control to edit canvas apps (experimental)](https://learn.microsoft.com/en-us/power-apps/maker/canvas-apps/git-version-control) and has several limitations, such as not allowing custom components, which we will most likely use.

Therefore, this repo will include the downloaded PowerApps bundle of the demo app, which can be uploaded into the PowerApps editor.

The app will contain a simple table component which shows the double payment candidates, and a component which can set the judgement status of one candidate pair at a time.
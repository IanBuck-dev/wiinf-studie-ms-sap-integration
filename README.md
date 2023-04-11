# Study - Integration of 3rd party BI tool into Microsoft Power Apps

This repo is part of a feasibility study which explores the integration of third party applications, in this case a business intelligence tool for detecting double payments for invoices, into the Microsoft ecosystem.

In this study, the focus will be on the integration with PowerApps.

## API - Simple Backend for double payment candidates

The main part of this repo is the API, a small backend with C# and .NET, that offers multiple endpoints to fetch double payment candidates and set their judgement status.
There is no endpoint yet to import double payment candidates given SAP login information, we simply wrote a little script that seeds the included SQLite database for demo purposes.

### Features:
Endpoints:
- GET of list of double payment candidate pairs
- GET of single double payment candidate pair
- POST to change the judgement status of a candidate pair

Others:
- simple API key authorization
- database seeding logic for dummy data
- for list endpoint:
  - ordering
  - filtering
  - pagination

Possible improvements:
- more sophisticated filtering with IQueryable (also needs move to EF Core) (f.e. via [Linq to Query string](http://linqtoquerystring.net/))
- logic to handle different tenant/accounts etc.
- ...

The API can be started locally via a tool such as [ngrok](https://ngrok.com/) to expose a online reachable url that can be used in the demo app like: `https://cc8c-2a04-4540-b04-8f00-c842-a24-3793-c0ec.eu.ngrok.io`

```
ngrok http 5257
```
(this starts ngrok and forwards requests to `http://localhost:5257`)

## PowerApps - Demo App

Unfortunately, using git with PowerApps is only available as a experimental feature at the moment: [Use Git version control to edit canvas apps (experimental)](https://learn.microsoft.com/en-us/power-apps/maker/canvas-apps/git-version-control) and has several limitations, such as not allowing custom components, which we will most likely use.

Therefore, this repo will include the downloaded PowerApps bundle of the demo app, which can be uploaded into the PowerApps editor.

## PowerApps - Custom API Connector

In order to connect a REST API into PowerApps (without any sort of published commercial connector), you need to configure a [Custom Connector](https://learn.microsoft.com/en-us/connectors/custom-connectors/define-openapi-definition) f.e. by providing an [OpenAPI doc](https://swagger.io/specification/)(PowerApps currently only support OpenAPI v2) which can then be further configured (f.e. by adding identifiers for the actions to be referenced inside the PowerApp).

This repo contains the configuration of the custom connector that can be imported via the provided json.

## PowerApps - Solution

This is the final export of the created solution.
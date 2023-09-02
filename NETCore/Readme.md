# ASP.NET Core WebApi Sample with HATEOAS, Versioning & Swagger

In this repository I want to give a plain starting point at how to build a WebAPI with ASP.NET Core.

This repository contains a controller which is dealing with ShoeItems. You can GET/POST/PUT/PATCH and DELETE them.

Hope this helps.

See the examples here: 

## Versions

``` http://localhost:29435/swagger ```

![ASPNETCOREWebAPIVersions](./.github/versions.jpg)

## GET all Shoes

``` http://localhost:29435/api/v1/shoes ```

![ASPNETCOREWebAPIGET](./.github/get.jpg)

## GET single shoe

``` http://localhost:29435/api/v1/shoes/2 ```

![ASPNETCOREWebAPIGET](./.github/getSingle.jpg)

## POST a shoeItem

``` http://localhost:29435/api/v1/shoes ```

```javascript
  {
      "name": "Lasagne",
      "type": "Main",
      "calories": 3000,
      "created": "2017-09-16T17:50:08.1510899+02:00"
  }
```

![ASPNETCOREWebAPIGET](./.github/post.jpg)

## PUT a shoeItem

``` http://localhost:29435/api/v1/shoes/5 ```

``` javascript
{
    "name": "Lasagne2",
    "type": "Main",
    "calories": 3000,
    "created": "2017-09-16T17:50:08.1510899+02:00"
}
```

![ASPNETCOREWebAPIGET](./.github/put.jpg)


## PATCH a shoeItem

``` http://localhost:29435/api/v1/shoes/5 ```

``` javascript
[
  { "op": "replace", "path": "/name", "value": "mynewname" }
]
```

![ASPNETCOREWebAPIGET](./.github/patch.jpg)

## DELETE a shoeItem

``` http://localhost:29435/api/v1/shoes/5 ```


![ASPNETCOREWebAPIGET](./.github/delete.jpg)

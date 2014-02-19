namespace FsWeb.Controllers

open System.Collections.Generic
open System.Web
open System.Web.Mvc
open System.Net.Http
open System.Web.Http
open FsWeb.Models
open System.Data
open System.Data.Linq
open Microsoft.FSharp.Data.TypeProviders
open Microsoft.FSharp.Linq

// USE [Flores_Demo]
// CREATE TABLE [dbo].[Persons](
// 	[ID] [char](10),
// 	[LastName] [varchar](30) ,
// 	[FirstName] [varchar](20)
// )

type dbSchema = SqlDataConnection<"Data Source=.;Initial Catalog=Flores_Demo;Integrated Security=SSPI;">

type ContactsController() =
    inherit ApiController()

    let db = dbSchema.GetDataContext()

    let persons =
        query {
            for row in db.Persons do
            select row
        }

    // GET /api/contacts
    // http://msdn.microsoft.com/en-us/library/hh361033.aspx & http://msdn.microsoft.com/en-us/library/dd233209.aspx
    member x.Get() = 
        seq { for row in persons -> new Contact(FirstName = row.FirstName, LastName = row.LastName, Phone = "123 456 789" ) }

    // POST /api/contacts
    member x.Post ([<FromBody>] contact:Contact) = 
        let newPerson = new dbSchema.ServiceTypes.Persons( FirstName=contact.FirstName, LastName=contact.LastName )
        db.Persons.InsertOnSubmit(newPerson)
        db.DataContext.SubmitChanges()
        x.Get()
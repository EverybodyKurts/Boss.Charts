module App

open Fable.Core
open Feliz

open Cockpit
open Cockpit.SD

let sdBand =
    { Label = (Label "-3 sd")
      Height = (Height 50.0)
      BackgroundColor = colors.Black }

let sdObj = Cockpit.SD.toJS sdBand

JS.console.log(sdObj)

[<ReactComponent>]
let HelloWorld () = Html.h1 $"Hello world {sdObj}"

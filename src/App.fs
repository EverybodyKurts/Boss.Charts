module App

open Fable.Core
open Feliz

open Cockpit
open Cockpit.SD

// import Chart from 'chart.js'
[<Import("Chart", from="chart.js")>]
let chartJs: obj = jsNative

let sdBand =
    { Label = (Label "-3 sd")
      Height = (Height 50u)
      BackgroundColor = colors.Black }

let sdObj = Cockpit.SD.toJS sdBand

JS.console.log (chartJs)

let foo = {| name = "foo" |}
let bar = {| place = "bar" |}

let baz = {|
  things = ([foo; bar] : obj list)
|}

JS.console.log (baz)

[<ReactComponent>]
let HelloWorld () = Html.h1 $"Hello world {sdObj}"

module App

open Feliz

open Color

let color = Color.create (Red 0) (Green 255) (Blue 255)
let colorString = Color.toRGBAstring color

[<ReactComponent>]
let HelloWorld() = Html.h1 $"Hello world {colorString}"
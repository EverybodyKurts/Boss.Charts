module Cockpit

open Color

let colors =
    {| Red = Color.create (Red 255) (Green 0) (Blue 0)
       Yellow = Color.create (Red 255) (Green 255) (Blue 0)
       Green = Color.create (Red 0) (Green 255) (Blue 0)
       Black = Color.create (Red 0) (Green 0) (Blue 0)
       DarkGreen = Color.create (Red 40) (Green 167) (Blue 69)
       Mustard = Color.create (Red 255) (Green 193) (Blue 7)
       Blue = Color.create (Red 23) (Green 162) (Blue 1) |}

type Label = Label of string
type Height = Height of float
type Stack = Stack of string
type XAxisID = XAxisID of string

type Band =
    { Label: Label
      Height: Height
      BackgroundColor: Color }

type InventoryTarget = InventoryTarget of int
type OrderPoint = OrderPoint of int

module SD =
    let toJS band =
        let (Label label) = band.Label
        let (Height height) = band.Height
        let color = band.BackgroundColor

        {| label = label
           data = [| height |]
           stack = "sd"
           xAxisID = "std-dev"
           backgroundColor = toRGBAstring color |}

module Inventory =
    let toJS band =
        let (Label label) = band.Label
        let (Height height) = band.Height
        let color = band.BackgroundColor

        {| label = label
           data = [| height |]
           backgroundColor = toRGBAstring color
           xAxisID = "inventory" |}

module InventoryTargetLine =
    let toJS (InventoryTarget it) =
        {| label = "Inventory Target"
           ``type`` = "line"
           xAxisID = "std-dev"
           borderColor = "black"
           borderWidth = 2 
           fill = false
           data = [| it; it |] |}

module OrderPointLine =
    let toJs (OrderPoint op) =
        {| label = "Order Point"
           ``type`` = "line"
           xAxisID = "std-dev"
           borderColor = toRGBAstring colors.Mustard
           borderWidth = 2
           fill = false 
           data = [| op; op |] |}
[<Measure>] type minute
[<Measure>] type hour
[<Measure>] type day
[<Measure>] type person
[<Measure>] type vaccination

let minPerHour = 60.0<minute/hour>
let hoursPerDay = 24.0<hour/day>
let minPerDay = minPerHour * hoursPerDay

let adminTime = 10.0<minute/vaccination>
let adminNumber = 2.0<vaccination/person>
let totalAdminTime = adminTime * adminNumber

let usPopulation = 328_200_000.0<person>
let herdImmunity = 0.7 * usPopulation

let daysToHerdImmunity = herdImmunity * totalAdminTime / minPerDay

daysToHerdImmunity / 10_000.0<person>

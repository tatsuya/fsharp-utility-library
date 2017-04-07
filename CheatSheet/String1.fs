﻿module String1

open Microsoft.VisualBasic
open System
open System.Text.RegularExpressions

let split (s : string) (separator : string) =
    s.Split([| separator |], StringSplitOptions.None)

let getBetween s from to1 inclusive =
    let m = Regex.Match(s, (if inclusive then sprintf "(%s.*?%s)" from to1 else sprintf "%s(.*?)%s" from to1), RegexOptions.IgnoreCase)
    if m.Success then Some (m.Groups.[1].Value) else None

// https://msdn.microsoft.com/en-us/library/ms912047.aspx
let fullwidth s = Strings.StrConv(s, VbStrConv.Wide, 1041)

// https://msdn.microsoft.com/en-us/library/ms912047.aspx
let halfwidth s = Strings.StrConv(s, VbStrConv.Narrow, 1041)
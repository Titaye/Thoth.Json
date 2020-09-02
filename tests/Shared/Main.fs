module Tests.Main

#if THOTH_JSON && FABLE_COMPILER
open Thoth.Json
open Fable.Mocha
#endif

#if THOTH_JSON && !FABLE_COMPILER
open Thoth.Json
open Expecto
#endif

#if THOTH_JSON_FABLE
open Thoth.Json.Fable
open Fable.Mocha
#endif

#if THOTH_JSON_NEWTONSOFT
open Thoth.Json.Newtonsoft
open Expecto
#endif

open Types

let quicktests =
    testList "QuickTest" [
        testList "Fake category" [
            testCase "QuickTest: #1" <| fun _ ->
                let jsonRecord =
                    """{ "a": 1.0,"b": 2.0,
                         "c": 3.0,
                         "d": 4.0,
                         "e": 5.0,
                         "f": 6.0,
                         "g": 7.0,
                         "h": 8.0 }"""


                let actual = Decode.unsafeFromString(Decode.field "b" Decode.float) jsonRecord
                Expect.equal actual 2.0 ""
                ()
        ]
    ]

[<EntryPoint>]
let main args =
    let allTests =
        testList "All" [
            Decoders.Manual.tests
            Decoders.Auto.tests
            Encoders.Manual.tests
            Encoders.Auto.tests
//            // Uncomment this line if you want to use the quicktests useful
//            // when prototyping or trying to reproduce an issue
            quicktests
        ]

    #if FABLE_COMPILER
    Mocha.runTests allTests
    #else
    runTestsWithArgs defaultConfig args allTests
    #endif

INCLUDE globals.ink

-> main

=== main ===
{tutorialSecretarySpoken: -> tutorialDone | -> tutorialNotDone }

-> END

=== tutorialNotDone ===
Secretary: Hi! You must be the new guy in town.
Secretary: I am the only secretary working here.
Secretary: I help the mayor with organising events, scheduling appointments, and more.
Ava: You may call me Ava.

~ tutorialSecretarySpoken = true

-> tutorialDone

=== tutorialDone ===
Ava: Anything you would like to ask?
    * [What is this place?]
        Ava: This is the mayor's house.
        -> DONE
    * [How do I go back to Atlas?]
        Ava: I do not know what that place is.
        -> DONE
    * [Leave]
        Ava: Bye!
        -> DONE

-> END
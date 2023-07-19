INCLUDE globals.ink
EXTERNAL QuestCompleted()
-> main

=== main ===
Eve: Hi there! What can I get for you?
    * [I want to buy something.]
        Eve: Sure. This is what we have in stock.
        -> DONE
    * [Do you have any quests?]
        Eve: None at the moment.
        -> DONE
    * [Leave]
        Eve: Goodbye.
        -> DONE

-> END
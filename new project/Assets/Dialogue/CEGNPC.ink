INCLUDE globals.ink
-> main

=== main ===
Hmmm... How much ram will be needed to run this in my computer...
Oh! Hi there!
    
[What's up?]
      Nothing much! Just wondering what I should do for my next project!-> DONE
[Do you have any quests?]{allQuestDone:I do not require assistance for now.|{(!CEGValidTime): Thanks for the help! Maybe come another time. |{CEGQuestStarted:I have already given you a quest. -> in_quest|{(!questCG1111ADone): -> CG1111AConvo |{(!questEG1311Done): -> EG1311Convo |{(!questCG2111ADone): -> CG2111AConvo}}}}}}-> DONE
[Leave]
  Goodbye.-> DONE
-> END

=== CG2111AConvo ===
{(!CEGQuestDone):
OK!!! The final product is done!!!!
This will be the final request >.< !!!
The final test will be to check whether all the features of my robot are present!
We will be running similar tests as the last time BUTTTTT this time you will have to find out the color that my robot detects using the given RGB values!
It will be a maze obstacle where it will have to navigate through! Don't worry! You got this!
So do you think you can help me with the final test??
-> start_quest("CG2111A", "Test my final product at one of the levels in the cave of the desert west of this village!")
->END
}
Finally! My robot works!! Without you I don't think I would have done it easily!!
With this robot I can defend the village against the monsters!!
Maybe I can even use it to investigate about the new SU Monster I have been hearing....
Anyway, Thanks for the help!
~questCG2111ADone = true
~CEGValidTime = false
->END


=== EG1311Convo ===
{(!CEGQuestDone):
    Thanks for the help with my prototype previously! Now I need to test it out further!
    I need it to overcome an obstacle that I created in one of the levels of the cave in the desert west of this village!
    It is some simple obstacle where the robot prototype has to go up and down a slope followed by throwing a rock at a target!
    -> start_quest("EG1311", "Use my prototype to overcome one of the obstacle in the cave of the desert west of village!")
    -> END
    }
    LEZGOOOOO!!!! It seems like my prototype is smurfing through these test cases!!
    I think I am gonna build the final product and give it a final test next time!
    Thanks for the help!!! Once I finish building the final product you can come and find me! Till next time!
     ~questEG1311Done = true
     ~CEGValidTime = false
->END


=== CG1111AConvo ===
{(!CEGQuestDone):
    You must be the new person in town! My name is TeeHaw.
    I am a computer engineer! 
    Based on your facial expression you don't seem to be surprised! That must mean there are computer engineers in your world!
    Anyway, I have been working on a project recently and I need some help!
    I need you to help me with testing a prototype of mine! I placed it in one of the level in the cave of the dessert!
    Can you help me make sure you pass the test puzzles that I've made using my prototype?
    Don't worry! It's a simple puzzle where you have to control the prototype robot and sense the correct colors on the walls!
    -> start_quest("CG1111A", "Test the prototype that I have placed in the cave in the desert west of village!")
    ->END
    }
     Amazing! My prototype seems to be working! Thanks for the help! Maybe I can use this prototype to defend against the monsters outside of our village!
     ~questCG1111ADone = true
     ~CEGValidTime = false
    -> END

=== in_quest ===
[What am I supposed to do again?]{questDesc}-> DONE
[Leave]
  Goodbye.-> DONE

-> END

=== start_quest(Name, Desc) ===
[Yes]
  Great!~ CEGQuestName = Name~ CEGQuestDesc = CEGQuestStarted = true-> DONE
[No]
  Sleep with one eye open :)-> DONE
-> END
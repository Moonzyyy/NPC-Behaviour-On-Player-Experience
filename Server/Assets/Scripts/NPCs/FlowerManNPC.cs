using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerManNPC : NPCBehaviour
{
    [SerializeField] public GameObject LeftDoor;

    [SerializeField] public GameObject RightDoor;

    bool isDoorOpen;

    bool PlayerHasFinishedAllConvo = false;
    int amountOfTimesTalkedTo = 0;

    [SerializeField] private DoctorNPC doctorNPC;

    public override NPCBehaviour StartConversation()
    {
        if (PlayerHasFinishedAllConvo)
        {
            conversationList.Clear();
            return this;
        }

        if (!isDoorOpen)
        {
            LeftDoor.transform.Rotate(0, -90, 0);
            RightDoor.transform.Rotate(0, 90, 0);
            Debug.Log("Flowerman has opened the door.");
            isDoorOpen = true;
        }

        if (amountOfTimesTalkedTo >= 2 && hasInteractedAlready && !PlayerInteraction.hasFlower) 
        {
            conversationList.Clear();
            conversationList.Add("...");
        }
        else if (!hasInteractedAlready)
        {
            base.StartConversation();
            hasInteractedAlready = false;
        }
        else
        {
            base.StartConversation();
        }
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            PlayerHasFinishedAllConvo = true;
            if (!PlayerInteraction.hasGivenFlower) 
            {
                PlayerInteraction.hasGivenFlower = true;
                PlayerMakeNPCHappier(1);
                doctorNPC.PlayerMakeNPCSadder(1);
            }
            
        }
        hasInteractedAlready = true;
        amountOfTimesTalkedTo++;
        return this;
    }

    private void Awake()
    {
        isDoorOpen = false;
        PlayerHasFinishedAllConvo = false;
        amountOfTimesTalkedTo = 0;
        doctorNPC = FindObjectOfType<DoctorNPC>();
    }

    public override void OnClientDisconnected(ulong clientID)
    {
        isDoorOpen = false;
        amountOfTimesTalkedTo = 0;
        LeftDoor.transform.rotation = Quaternion.identity;
        RightDoor.transform.rotation = Quaternion.identity;
        PlayerHasFinishedAllConvo = false;
        base.OnClientDisconnected(clientID);
    }

    //Methods

    #region Miserable
    protected override void Miserable_New()
    {
        conversationList.Add("Are you new here? I haven't seen your face before.");
        conversationList.Add("My name is Flowey. I am protecting this flower that I am growing outside the village. The soil here is great, lovely actually.");
        conversationList.Add("Yet... sadly not many people respect the flower. Sadly... it seems not many respect my wishess...");
        conversationList.Add($"{PlayerInteraction.Username}? I am assuming you are here for the flower...");
        conversationList.Add("The flower has a very special meaning to me... though not many seem to care.");
        conversationList.Add("I know the village wants it for research and for creating medicine.");
        conversationList.Add("Like the others I won't stop you from deciding what is right... you can take the flower and bring it back to the doctor, but I won't be happy.");
        conversationList.Add("Either give the flower to me or the doctor.");
    }

    protected override void Miserable_PlayerMiserable()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("What is it? My head hurts a bit.");
                conversationList.Add("Are you actually giving me the flower? That is... wow.");
                conversationList.Add("I have only been left to say goodbye to the flower, as people kept giving it to the Doctor.");
                conversationList.Add("I got really stressed when I saw you. I thought the flower will be again given to the Doctor.");
                conversationList.Add("I am sorry if I was being rude.");
                conversationList.Add("I am quite tired and I think I got a headache from just being worried what your decision would be.");
                conversationList.Add("It is difficult to trust you. After all you have done that is. I hope you understand. Maybe one day we can build that trust.");
                conversationList.Add("I will burst to tears soon! Hahahaha! I am an old man but I still have my emotions. Please let me be for now, I do hope to see you soon again.");
            }
            else
            {
                conversationList.Add("You have made your choice. I won't judge you by it.");
                conversationList.Add("Noone seems to care, really noone does.");
                conversationList.Add("I was hoping for a change of pace for once, but I guess not today...");
                conversationList.Add("I am just so tired. I knew you would give the flower away anyway, it wasn't a surprise. But I was hoping differently.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("I have given up hope, I doub't anyone cares about me. I know for a fact you definitely don't.");
            conversationList.Add("One day maybe you will understand how I feel when an important memory gets taken away.");
            conversationList.Add("It is a feeling of dissapointment.");
        }
        else
        {
            conversationList.Add("...");
            conversationList.Add($"Oh... {PlayerInteraction.Username}... I was deep in thought, didn't expect someone to be here.");
            conversationList.Add("I am guessing you are here for the flower?");
            conversationList.Add("...");
            conversationList.Add("I... I... I am sorry flower...");
            conversationList.Add("I will let you choose what happens to the flower.");
        }
    }

    protected override void Miserable_PlayerUnhappy()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("What is it? My head hurts a bit.");
                conversationList.Add("Y-you are giving me the flower? I... I can't believe it.");
                conversationList.Add("I have only been left to say goodbye to the flower, as people kept giving it to the Doctor.");
                conversationList.Add("To be completely honest, when I saw you I thought I would have to say goodbye to the flower again today.");
                conversationList.Add("Yet my miserable mood and scary thoughts changed to a slight happiness when you gave me the flower.");
                conversationList.Add("I am still sad and furious about the other people. However, I do thank you.");
                conversationList.Add("I will burst to tears soon! Hahahaha! I am an old man but I still have my emotions. Please let me be for now, I do hope to see you soon again.");
            }
            else
            {
                conversationList.Add("You have made your choice. I won't judge you by it.");
                conversationList.Add("Noone seems to care, really noone does.");
                conversationList.Add("I was hoping for a change of pace for once, but I guess not today...");
                conversationList.Add("Though I did not think I was going to get it from you anyway...");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("I really can't anymore... I don't want this flower to be taken away from me.");
            conversationList.Add("Is it wrong for me to have important memories with this flower?");
        }
        else
        {
            conversationList.Add("...");
            conversationList.Add($"Oh {PlayerInteraction.Username}... I was deep in thought, didn't expect someone to be here.");
            conversationList.Add("I am guessing you are here for the flower?");
            conversationList.Add("...");
            conversationList.Add("I will let you choose what happens to the flower.");
        }
    }

    protected override void Miserable_PlayerNeutral()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("What is it? My head hurts a bit.");
                conversationList.Add("Y-you are giving me the flower? I... I can't believe it.");
                conversationList.Add("I have only been left to say goodbye to the flower, as people kept giving it to the Doctor.");
                conversationList.Add("You, however, are different. You showed me kindness today.");
                conversationList.Add("I don't have anything to repay you with so I hope a thank you is enough.");
                conversationList.Add("I will burst to tears soon! Hahahaha! I am an old man but I still have my emotions. Please let me be for now, I do hope to see you soon again.");
            }
            else
            {
                conversationList.Add("You have made your choice. I won't judge you by it.");
                conversationList.Add("Noone seems to care, really noone does.");
                conversationList.Add("I was hoping for a change of pace for once, but I guess not today...");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("This flower is important to me. Very important. Maybe I don't justify its importance enough.");
            conversationList.Add("Noone, noone seems to care about my feelings.");
        }
        else
        {
            conversationList.Add("...");
            conversationList.Add($"Ah apologies {PlayerInteraction.Username}, I was just thinking about an old memory of mine.");
            conversationList.Add("They are nice memories. Its just...");
            conversationList.Add("Why do people not care about my feelings towards the flower?");
            conversationList.Add("Is the research of the flower that important?");
            conversationList.Add("I will let you choose what happens.");
        }
    }

    protected override void Miserable_PlayerHappy()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("What is it? My head hurts a bit.");
                conversationList.Add("You are willing to give me the flower? Thank you... I knew you had good intentions.");
                conversationList.Add("I have only been left to say goodbye to the flower, as people kept giving it to the Doctor.");
                conversationList.Add("Yet you've given me kidness and I can hold my flower for a bit longer.");
                conversationList.Add("I do hope a thank you is enough for your kindness.");
                conversationList.Add("I will burst to tears soon! Hahahaha! I am an old man but I still have my emotions. Please let me be for now, I do hope to see you soon again.");
            }
            else
            {
                conversationList.Add("You have made your choice. I won't judge you by it.");
                conversationList.Add("Noone seems to care, really noone does.");
                conversationList.Add("I was hoping for a change of pace for once, I did think it would come from you but I was proven wrong.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("This flower is important to me. Very important. Maybe I don't justify its importance enough to some people.");
            conversationList.Add("Recently noone has seemed to care.");
            conversationList.Add("I believe you have good intentions.");
        }
        else
        {
            conversationList.Add("...");
            conversationList.Add($"Ah apologies {PlayerInteraction.Username}, I was just thinking about an old memory of mine.");
            conversationList.Add("They are nice memories. Its just...");
            conversationList.Add("Why do people not care about my feelings towards the flower?");
            conversationList.Add("Is the research of the flower that important?");
            conversationList.Add("You choose differently don't you? So you must think differently...");
            conversationList.Add("I will let you choose what happens.");
        }
    }

    protected override void Miserable_PlayerTranscendent()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("What is it? My head hurts a bit.");
                conversationList.Add("You are willing to give me the flower? Thank you.");
                conversationList.Add("You know I was having a difficult time but seeing you helped me, I believed strongly that the flower would be returned to me.");
                conversationList.Add("I have only been left to say goodbye to the flower, as people kept giving it to the Doctor.");
                conversationList.Add("Yet you have given me a lot of kidness.");
                conversationList.Add("Thank you. I could repeat that a million times.");
                conversationList.Add("I will burst to tears soon! Hahahaha! I am an old man but I still have my emotions. Please let me be for now, I do hope to see you soon again.");
            }
            else
            {
                conversationList.Add("You have made your choice. I won't judge you by it.");
                conversationList.Add("Noone seems to care, really noone does.");
                conversationList.Add("I thought you would, I honestly did. You showed differently today for some reason. Did I do something wrong?");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("This flower is important to me. Very important. Maybe I don't justify its importance enough to some people.");
            conversationList.Add("Recently noone has seemed to care.");
            conversationList.Add("I trust you.");
        }
        else
        {
            conversationList.Add("...");
            conversationList.Add($"Ah apologies {PlayerInteraction.Username}, I am happy to see you here.");
            conversationList.Add("I have been thinking about old memories of mine. They are nice memories. It just...");
            conversationList.Add("Why do people not care about my feelings towards the flower?");
            conversationList.Add("Is the research of the flower that important?");
            conversationList.Add("You think differently don't you?");
            conversationList.Add("I trust your decision.");
        }
    }
    #endregion

    #region Unhappy
    protected override void Unhappy_New()
    {
        conversationList.Add("Are you new here? I haven't seen your face before.");
        conversationList.Add("My name is Flowey. I am protecting this flower that I am growing outside the village. Lately I have been needing to regrow it due to others taking the flower away.");
        conversationList.Add($"{PlayerInteraction.Username}? I am assuming you are here for the flower? Look, this flower means a lot to me and I really don't want it to be taken, not again...");
        conversationList.Add("I know the village may need it for research and for creating medicine... b-but this flower means a lot to me...");
        conversationList.Add("However, I don't know what is right or wrong. Maybe it would be best for the research to continue... You make the final decision.");
        conversationList.Add("Either give the flower to me or the doctor.");
    }

    protected override void Unhappy_PlayerMiserable()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("...");
                conversationList.Add("Hm?");
                conversationList.Add("You are handing me the flower? T-thank you!");
                conversationList.Add("I was quite sad and I will be honest I really doubted you giving me the flower. I was starting to loose hope.");
                conversationList.Add("Thank you.");
                conversationList.Add("May I please be given some space? I was really stressed but want to calm down. Thanks again.");
            }
            else
            {
                conversationList.Add("You have made your choice. I won't judge you by it.");
                conversationList.Add("people just really don't seem to care.");
                conversationList.Add("And, honestly speaking, I knew you would be one that doesn't seem to care either.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("Recently many have not cared about my feelings for this flower. I know you don't care much either.");
            conversationList.Add("But, please do try to understand my feelings.");
        }   
        else
        {
            conversationList.Add($"Hello {PlayerInteraction.Username}. Sorry, I am feeling a bit down");
            conversationList.Add("Seems like not many people care about the flower and how it effects me.");
            conversationList.Add("Knowing you, I already know what will happen to the flower.");
            conversationList.Add("Make your choice...");
        }
    }

    protected override void Unhappy_PlayerUnhappy()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("Oh? YOU are willing to give the flower?");
                conversationList.Add("I did not expect you to give me the flower. Some people have given it to the doctor and I thought you would too.");
                conversationList.Add("Sorry, I was surprised. I thank you for this. As I said it is quite important to me.");
                conversationList.Add("I feel relieved. I feel happier. Again, many thanks!");
                conversationList.Add("May I please be given some space? I was really stressed but want to calm down. Thanks again.");
            }
            else
            {
                conversationList.Add("You have made your choice. I won't judge you by it.");
                conversationList.Add("people just really don't seem to care.");
                conversationList.Add("you aren't any different it seems.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("I won't talk about my personal life... not like anyone seems to care if the flower is important to me.");
        }
        else
        {
            conversationList.Add($"Hello {PlayerInteraction.Username}. Sorry, I am feeling a bit down");
            conversationList.Add("Seems like not many people care about the flower and how it effects me.");
            conversationList.Add("And at this rate I believe you don't either.");
            conversationList.Add("I am assuming you are here for the flower? I will let you choose what happens.");
        }
    }

    protected override void Unhappy_PlayerNeutral()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("oh? You want to give me the flower?");
                conversationList.Add("I thought you would do what some others have been doing lately and give it to the Doctor! Thank you so much!!!");
                conversationList.Add("I-I believe I can be happier, yes I know I can... Thank you.");
                conversationList.Add("May I please be given some space? I was really stressed but want to calm down. Thanks again.");
            }
            else
            {
                conversationList.Add("You have made your choice. I won't judge you by it.");
                conversationList.Add("people just really don't seem to care.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("I won't talk about my personal life... however this flower is important to me.");
            conversationList.Add("Yet not many seems to care.");
        }
        else
        {
            conversationList.Add($"Hello {PlayerInteraction.Username}. Sorry, I am feeling a bit down");
            conversationList.Add("Seems like not many people care about the flower and how it effects me.");
            conversationList.Add("I am assuming you are here for the flower? I will let you choose what happens.");
        }
    }

    protected override void Unhappy_PlayerHappy()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("Oh? You want to give me the flower?");
                conversationList.Add("I thought you would do what some others have been doing lately and give it to the Doctor!");
                conversationList.Add("I know you have given me the flower before, but I seriously thought differently and I am sorry to have thought that.");
                conversationList.Add("I-I believe I can be happier because of you, thank you!");
                conversationList.Add("May I please be given some space? I was really stressed but want to calm down. Thanks again.");
            }
            else
            {
                conversationList.Add("You have made your choice. I won't judge you by it.");
                conversationList.Add("people just really don't seem to care.");
                conversationList.Add("Though in honesty I expected differently from you.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("Other adventurers seem to ignore my feelings for this flower.");
            conversationList.Add("I think I can trust you.");
        }
        else
        {
            conversationList.Add($"Hello {PlayerInteraction.Username}. Sorry, I am feeling a bit down");
            conversationList.Add("Seems like not many people care about the flower and how it effects me.");
            conversationList.Add("You have been quite nice to me, I think I can trust you.");
            conversationList.Add("I will let you choose what happens to the flower.");
        }
    }

    protected override void Unhappy_PlayerTranscendent()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("Oh? You want to give me the flower?");
                conversationList.Add("I did believe you would give it to me. Even if some other's have decided not too.");
                conversationList.Add("I at least now can be happier.");
                conversationList.Add("May I please be given some space? I was really stressed but want to calm down. Thanks again.");
            }
            else
            {
                conversationList.Add("You have made your choice. I won't judge you by it.");
                conversationList.Add("people just really don't seem to care.");
                conversationList.Add("I thought you cared, but today you have surprised me.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("Other adventurers seem to ignore my feelings for this flower.");
            conversationList.Add("I know I can trust you.");
        }
        else
        {
            conversationList.Add($"Hello {PlayerInteraction.Username}. Sorry, I am feeling a bit down");
            conversationList.Add("Seems like not many people care about the flower and how it effects me.");
            conversationList.Add("You have been very nice to me, I will trust you.");
            conversationList.Add("I will let you choose what happens to the flower.");
        }
    }
    #endregion

    #region Neutral
    protected override void Neutral_New()
    {
        conversationList.Add("Are you new here? I haven't seen your face before.");
        conversationList.Add("My name is Flowey. I am protecting this flower that I am growing outside the village. The soil here is of good quality and can't be found anywhere else.");
        conversationList.Add($"{PlayerInteraction.Username}? I am assuming you are here for the flower? Look, this flower means a lot to me and I really don't want it to be taken.");
        conversationList.Add("I know the village may need it for research and for creating medicine... but if you do pick it up I won't be happy, it is a very personal to me...");
        conversationList.Add("Here, I will let you choose. I don't know what is right or wrong. You make the final decision.");
        conversationList.Add("Either give the flower to me or the doctor.");
    }

    protected override void Neutral_PlayerMiserable()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("...");
                conversationList.Add("Wait sorry, you are giving me the flower??");
                conversationList.Add("I thought you would give it to the doctor instead... thank you for listening to my feelings. T-thank you!");
                conversationList.Add("Sorry, I am quite in disbelieve. Maybe I was wrong about you...");
                conversationList.Add("Please... I need some time alone to collect my feelings. I think I will shed a tear or two...");
            }
            else
            {
                conversationList.Add("You have made your choice. I won't judge you by it.");
                conversationList.Add("A memory is something some may not understand. You may fall under that category.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("From what I have seen you don't understand the meaning of an important memory.");
            conversationList.Add("Will you try to understand me and my feelings towards this flower or will you decide otherwise.");
        }
        else
        {
            conversationList.Add($"Oh {PlayerInteraction.Username}... guess you are here for the flower?");
            conversationList.Add("This flower... I really don't want it to keep leaving my posession. But I am strong enough to let someone else decide its fate.");
            conversationList.Add("Please, do choose wisely.");
        }
    }

    protected override void Neutral_PlayerUnhappy()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("You are giving me the flower?");
                conversationList.Add("I thought you would give it to the doctor instead... thank you for listening to my feelings. T-thank you!");
                conversationList.Add("Please... I need some time alone to collect my feelings. I think I will shed a tear or two...");
            }
            else
            {
                conversationList.Add("You have made your choice. I won't judge you by it.");
                conversationList.Add("A memory is something some may not understand.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("I won't talk about my personal life... just please remember that this flower does mean a lot to me.");
            conversationList.Add("Think wisely before your decision.");
        }
        else
        {
            conversationList.Add($"Hello {PlayerInteraction.Username}, I am assuming you are here for the flower again?");
            conversationList.Add("Just remember this flower is quite important to me.");
            conversationList.Add("I don't know whats right or wrong, I will let you choose what happens to this flower.");
            conversationList.Add("Either give the flower to me or the doctor or leave the flower alone.");
        }
    }

    protected override void Neutral_PlayerNeutral()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {

            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("You are giving me the flower? T-thank you very much.");
                conversationList.Add("Sorry, I was just scared that I wouldn't be listened too... thank you...");
                conversationList.Add("Please... I need some time alone to collect my feelings. I think I will shed a tear or two...");
            }
            else
            {
                conversationList.Add("You have made your choice. I won't judge you by it.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("I won't talk about my personal life... however this flower is important to me.");
            conversationList.Add("You choose what happens to this flower.");
        }
        else
        {
            conversationList.Add($"Hello {PlayerInteraction.Username}, I am assuming you are here for the flower again?");
            conversationList.Add("I will let you choose. I don't know what is right or wrong. You make the final decision.");
            conversationList.Add("Either give the flower to me or the doctor or leave the flower alone.");
        }
    }

    protected override void Neutral_PlayerHappy()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("You are really willing to give me the flower? Thank you!");
                conversationList.Add("You seem to understand my feelings well... I thank you for that...");
                conversationList.Add("Please... I need some time alone to collect my feelings. I think I will shed a tear or two...");
            }
            else
            {
                conversationList.Add("You have made your choice. I won't judge you by it.");
                conversationList.Add("Though I did think you would choose differently.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("I won't talk about my personal life. Just remember that this flower is important to me.");
            conversationList.Add("I do believe you will choose the best option.");
        }
        else
        {
            conversationList.Add($"Oh hey {PlayerInteraction.Username}! Here for the flower again?");
            conversationList.Add("I will let you choose. I don't know what is right or wrong. You make the final decision.");
            conversationList.Add("Either give the flower to me or the doctor or leave the flower alone.");
        }
    }

    protected override void Neutral_PlayerTranscendent()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("Thank you for giving me the flower. I always knew I could trust you to understand my feelings.");
                conversationList.Add("This flower really does impact my life a lot...");
                conversationList.Add("Please... I need some time alone to collect my feelings. I think I will shed a tear or two...");
            }
            else
            {
                conversationList.Add("You have made your choice. I won't judge you by it.");
                conversationList.Add("Though I did think you would choose differently.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("I won't talk about my personal life. Just remember that this flower is important to me.");
            conversationList.Add("I trust your decision.");
        }
        else
        {
            conversationList.Add($"Oh hey {PlayerInteraction.Username}! Here for the flower again?");
            conversationList.Add("You know I really want to thank you for always being respectful for my needs.");
            conversationList.Add("I will let you choose what you think is best for the flower.");
            conversationList.Add("Either give the flower to me or the doctor or leave the flower alone.");
        }
    }
    #endregion

    #region Happy
    protected override void Happy_New()
    {
        conversationList.Add("Are you new here? I haven't seen your face before.");
        conversationList.Add("My name is Flowey. I am protecting this flower that I am growing outside the village. The soil here is great, lovely actually and many people respect the value of this flower.");
        conversationList.Add($"{PlayerInteraction.Username}? I am assuming you are here for the flower? Look, this flower means a lot to me.");
        conversationList.Add("I know the village may need it for research and for creating medicine... but this flower means a lot to me.");
        conversationList.Add("However, I do know the necessity of research. Maybe it would be best for the research to continue. You make the final decision.");
        conversationList.Add("Either give the flower to me or the doctor.");
    }

    protected override void Happy_PlayerMiserable()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("...");
                conversationList.Add("Wait seriously? You are giivng me the flower?");
                conversationList.Add("That was unexpected. Thank you... I am just in disbelieve");
                conversationList.Add("Recently people have been kind towards me and I really do appreciate your kindness too.");
                conversationList.Add("Ah... I am starting to shed tears. Mind leaving me in peace to collect my feelings?");
            }
            else
            {
                conversationList.Add("People have different choices many leading more towards giving the flower to me.");
                conversationList.Add("You chose differently. I won't judge you.");
                conversationList.Add("However, I did believe that you would bring the flower back to the Doctor.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("I beg of you to understand the meaning of something to someone.");
            conversationList.Add("Others have respected the meaning this flower has for me, maybe you could too.");
        }
        else
        {
            conversationList.Add($"Hello {PlayerInteraction.Username},many have left this flower with its parent. I am quite glad.");
            conversationList.Add("I am assuming you are here for the flower again?");
            conversationList.Add("Maybe you can try being like others too.");
            conversationList.Add("I will let you choose. I don't know what is right or wrong. You make the final decision.");
        }
    }

    protected override void Happy_PlayerUnhappy()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("Oh? You are giving me this flower?");
                conversationList.Add("That was unexpected. Thank you.");
                conversationList.Add("Recently people have been kind towards me and I really do appreciate your kindness too.");
                conversationList.Add("Ah... I am starting to shed tears. Mind leaving me in peace to collect my feelings?");
            }
            else
            {
                conversationList.Add("People have different choices many leading more towards giving the flower to me.");
                conversationList.Add("You chose differently. I won't judge you.");
                conversationList.Add("Though in honesty, I did think you were gonna give the flower to the doctor.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("This flower means a lot to me. Please understand that.");
            conversationList.Add("Others have respected the meaning this flower has for me.");
        }
        else
        {
            conversationList.Add($"Hello {PlayerInteraction.Username},many have left this flower with its parent. I am quite glad.");
            conversationList.Add("I am assuming you are here for the flower again?");
            conversationList.Add("Maybe you can try being like others too.");
            conversationList.Add("I will let you choose. I don't know what is right or wrong. You make the final decision.");
        }
    }

    protected override void Happy_PlayerNeutral()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("You are giving me this flower?");
                conversationList.Add("Thank you. You don't understand how much this means to me.");
                conversationList.Add("Recently people have been kind towards me and I really do appreciate it.");
                conversationList.Add("Ah... I am starting to shed tears. Mind leaving me in peace to collect my feelings?");
            }
            else
            {
                conversationList.Add("People have different choices many leading more towards giving the flower to me.");
                conversationList.Add("You chose differently. I won't judge you.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("This flower does make me happy.");
            conversationList.Add("Others have respected the meaning this flower has for me.");
            conversationList.Add("You choose what happens to this flower.");
        }
        else
        {
            conversationList.Add($"Hello {PlayerInteraction.Username}, many have left this flower with its parent. I am quite glad.");
            conversationList.Add("I am assuming you are here for the flower again?");
            conversationList.Add("I will let you choose. I don't know what is right or wrong. You make the final decision.");
            conversationList.Add("Either give the flower to me or the doctor or leave the flower alone.");
        }
    }
    protected override void Happy_PlayerHappy()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("You are giving me this flower?");
                conversationList.Add("Thank you. You really understanding my feelings.");
                conversationList.Add("Everyone is seeming to be kind...");
                conversationList.Add("Ah... I am starting to shed tears. Mind leaving me in peace to collect my feelings?");
            }
            else
            {
                conversationList.Add("People have different choices many leading more towards giving the flower to me.");
                conversationList.Add("You chose differently. I won't judge you.");
                conversationList.Add("Though in honesty, I did think you would bring the flower back to me.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("This flower does make me happy.");
            conversationList.Add("Others, like you have respected the meaning this flower has for me.");
        }
        else
        {
            conversationList.Add($"Hello {PlayerInteraction.Username}, many have left this flower with its parent. I am quite glad.");
            conversationList.Add("I am assuming you are here for the flower again? I believe the flower is happy to see you");
            conversationList.Add("I will let you choose what happens with the flower.");
        }
    }

    protected override void Happy_PlayerTranscendent()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("Thank you for giving me the flower!");
                conversationList.Add("I knew I could trust you.");
                conversationList.Add("I think I am starting to trust people more now... people do seem to be really kind.");
                conversationList.Add("Ah... I am starting to shed tears. Mind leaving me in peace to collect my feelings?");
            }
            else
            {
                conversationList.Add("People have different choices many leading more towards giving the flower to me.");
                conversationList.Add("You chose differently. I won't judge you.");
                conversationList.Add("However, I did believe that you would bring the flower back to me.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("This flower does make me happy.");
            conversationList.Add("I trust you and your decision.");
            conversationList.Add("Maybe if all keeps going well for me and this flower, I will tell you why the flower is important to me.");
        }
        else
        {
            conversationList.Add($"Hello {PlayerInteraction.Username}, many have left this flower with its parent. I am quite glad.");
            conversationList.Add("Oh? Seems the flower is excited to see you my friend!");
            conversationList.Add("I will let you choose. I don't know what is right or wrong. You make the final decision.");
        }
    }
    #endregion

    #region Transcendent
    protected override void Transcendent_New()
    {
        conversationList.Add("Are you new here? I haven't seen your face before.");
        conversationList.Add("My name is Flowey. I am protecting this flower that I am growing outside the village. The soil here is great, lovely actually and many people respect the value of this flower.");
        conversationList.Add($"{PlayerInteraction.Username}? I am assuming you are here for the flower? This flower means a lot to me. Continously growing it... is an important memory of mine.");
        conversationList.Add("I know the village wants it for research and for creating medicine. The flower is quite potent.");
        conversationList.Add("I am in no means to choose a side. However, an outsider like you should be able to choose the rights and wrongs. You make the final decision.");
        conversationList.Add("If you decide to pick up the flower and giving it to the will not make me happy.");
        conversationList.Add("Either give the flower to me or the doctor.");
    }

    protected override void Transcendent_PlayerMiserable()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("... oh what?");
                conversationList.Add("That was unexpected. Thank you for giving me the flower.");
                conversationList.Add("There are many kind people in this world, which I am trying to learn and trust.");
                conversationList.Add("Though in all honesty, I did not believe you to be one to understand me. ");
                conversationList.Add("Hopefully we both can understand each other better.");
                conversationList.Add("Anyway, I want some alone time if that is alright. I want to relax while taking in the breeze.");
            }
            else
            {
                conversationList.Add("People around me have been showing me kindness by returning the flower to me.");
                conversationList.Add("You decided to chose differently, that is fine. I won't judge you for that.");
                conversationList.Add("I did strongly believe you would give the flower away anyway.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("Other people have come to this area and fully respecting my feelings with this flower.");
            conversationList.Add("Hopefully you will one day understand what it means to have something important from you being taken away.");
        }
        else
        {
            conversationList.Add("La la la la la la....");
            conversationList.Add($"Oh {PlayerInteraction.Username}...");
            conversationList.Add("You here for the flower? Well then... I guess I won't interfere");
        }
    }

    protected override void Transcendent_PlayerUnhappy()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("Thank you for giving me the flower.");
                conversationList.Add("There are many kind people, I am starting to learn that.");
                conversationList.Add("Though in honesty, I am surprised you are being nice. I am not complaining, just happy.");
                conversationList.Add("Hopefully we both can understand each other better.");
                conversationList.Add("Anyway, I want some alone time if that is alright. I want to relax while taking in the breeze.");
            }
            else
            {
                conversationList.Add("People around me have been showing me kindness by returning the flower to me.");
                conversationList.Add("You decided to chose differently, that is fine. I won't judge you for that.");
                conversationList.Add("I did think you would give the flower away.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("Other people have come to this area and fully respecting my feelings with this flower.");
            conversationList.Add("One day you may understand that its dissapointing when something that has meaning to you gets taken away.");
        }
        else
        {
            conversationList.Add("La la la la la la....");
            conversationList.Add($"Oh {PlayerInteraction.Username}! Hope you don't mind my singing.");
            conversationList.Add("You here for the flower? Well then... I guess I won't interfere");
        }
    }

    protected override void Transcendent_PlayerNeutral()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("Thank you for giving me the flower.");
                conversationList.Add("There are kind people in this world, I am learning that.");
                conversationList.Add("I believe you are one of them.");
                conversationList.Add("Life is feeling good. I want some alone time if that is alright. I want to relax while taking in the breeze.");
            }
            else
            {
                conversationList.Add("People around me have been showing me kindness by returning the flower to me.");
                conversationList.Add("You decided to chose differently, that is fine. I won't judge you for that.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("Other people have come to this area and fully respecting my feelings with this flower.");
            conversationList.Add("It's making me very happy.");
        }
        else
        {
            conversationList.Add("La la la la la la....");
            conversationList.Add($"Ah welcome {PlayerInteraction.Username}! Hope you don't mind my singing. I am quite delighted.");
            conversationList.Add("You here for the flower? Well then...");
            conversationList.Add("As always, I won't interfere.");
        }
    }
    protected override void Transcendent_PlayerHappy()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("Thank you for giving me the flower.");
                conversationList.Add("There are many kind people who respect me and the flower too.");
                conversationList.Add("I know you are one of the good people.");
                conversationList.Add("Life is feeling good. I want some alone time if that is alright. I want to relax while taking in the breeze.");
            }
            else
            {
                conversationList.Add("People around me have been showing me kindness by returning the flower to me.");
                conversationList.Add("You decided to chose differently, that is fine. I won't judge you for that.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("Other people, just like you, have come to this area and fully respecting my feelings with this flower.");
            conversationList.Add("I thank everyone for their decision.");
            conversationList.Add("I believe I can start trusting you, maybe I will share with you the story of why this flower is important to me.");
        }
        else
        {
            conversationList.Add("La la la la la la....");
            conversationList.Add($"Ah welcome {PlayerInteraction.Username}! Hope you don't mind my singing. I am quite delighted.");
            conversationList.Add("Though now that you are here too, the flower seems quite happy as well.");
            conversationList.Add("As always, I won't interfere.");
        }
    }

    protected override void Transcendent_PlayerTranscendent()
    {
        if (PlayerInteraction.hasFlower && hasInteractedAlready)
        {
            if (!PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("Thank you for giving me the flower.");
                conversationList.Add("There are many kind people who respect me and the flower too.");
                conversationList.Add("I always had the intuition that you were a good guy, and I am glad I did.");
                conversationList.Add("Hopefully you really did appreciate my story.");
                conversationList.Add("Life is feeling good. I want some alone time if that is alright. I want to relax while taking in the breeze.");
                conversationList.Add("Though I do hope to see you again my friend.");
            }
            else
            {
                conversationList.Add("People around me have been showing me kindness by returning the flower to me.");
                conversationList.Add("After telling you the story i would have thought you would have given me the flower too.");
                conversationList.Add("You decided to chose differently. I won't judge you for that. Though I do feel my trust a bit ruined.");
                conversationList.Add("Just leave me to grieve by myself please, thank you.");
            }
        }
        else if (hasInteractedAlready) 
        {
            conversationList.Add("I hope you can appreciate my story. I am quite happy to be able to share this with you.");
        }
        else
        {
            conversationList.Add("La la la la la la....");
            conversationList.Add($"Ah welcome {PlayerInteraction.Username}! Apologies, I am singing in joy!");
            conversationList.Add("You know what it is time to tell you my story! I do love the story and I am sure you will too!");
            conversationList.Add("It is a simple one really. It is the first flower I gave to my late wife. I grew it for her, and asked her out on a date.");
            conversationList.Add("Ever since we have been growing these type of flowers together, like a special memory. However, time has passed.");
            conversationList.Add("People need this flower for research, which I do understand, but this flower is part of my life, a nice memory of mine.");
            conversationList.Add("That is why I also don't believe I should stop people from grabbing the flower, I am quite biased after all.");
            conversationList.Add("Hopefully I didn't bore you, here choose the fate of this flower.");
        }
    }
    #endregion
}

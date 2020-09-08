# Unity-Project


Git workflow
No one is supposed to push directly to master! All code should go through pull-requests.

    1. Select a user story from Trello, or create one if needed! 
    2. It's important to change to the master branch before creating new branches $ git checkout master 
    3. Remember to update your local master branch before creating new branches $ git pull 
    4. Create your new branch $ git branch user_story_name 
    5. Change to your new branch $ git checkout user_story_name 
    6. Commit your local changes often $ git commit -am "descriptive message" 
    7. Push your changes to Github $ git push origin HEAD 
    8. Create a pull-request into master only when your user story is implemented and tested. 
    
The pull-request will start a new thread where you can discuss your implementation, request code review and merge your branch into master. You can delete your branch after it has been merged into master.

You can use $ git fetchto fetch a list of other people's branches.

[Feature Branch Workflow](https://www.atlassian.com/git/tutorials/comparing-workflows/feature-branch-workflow) 

Each new feature a person is working on should be made in a new branch. These branches should ideally never be more than a few days old as the features should be small and quick to implement. The person working on the feature will make a pull-request to merge the finished feature into the master branch once it's completed. This way the master branch will only ever contain working code.

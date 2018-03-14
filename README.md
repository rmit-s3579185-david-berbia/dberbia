# group-6-ai-games

## Git Workflow
___

With the exception of Zen's initial commit to Master, all commits should be made to your feature branch.

Our Branch Heirarchy is as follows:

Master --> Dev --> Feature_x

When your feature is complete, you can merge back into Dev using the process demonstrated below.

This demo use the command line but you can apply the same instructions to a Git GUI Client. I like to use Git Kraken on my mac. [Link Here](https://www.gitkraken.com/)

### Initial Setup

To clone the repo into a local directory, simply use `git clone https://github.com/andrew-finlayson/group-6-ai-games.git`

In order to check which branch you are on at any time, use `git status`. By default you should initially be on Master. 

To checkout the Dev branch, use `git checkout Dev`

Once you are on the Dev branch (remember you can check with `git status`), you will want to create a local branch for yourself to work on. 

### Working locally Within your branch

To create your new feature branch, use `git checkout -b feature_name`, where feature_name is the name of the feature you are working on.

When you are working in this branch, always remember to commit your changes as you go. This is much easier to do within a GUI, however from the command line...

To add the changed files/directories you can do so individually with `git add x.md dir test2.txt` and so on. OR, what I prefer to do is add all changed files or directories from the root if I am using the command line. This can be done with `git add .`

Once you have added the files you ave staged them for a commit, to commit use the command `git commit -m 'Your Commit Message'`.

### Pushing Changes to the remote repositiory

Note that this commit will only commit the changes to the local copy of your branch. In order to push your commits to the remote branch, you will then need to use `git push origin feature_name` this will not only push your commits to the remote git repo but will also create a remote copy of your feature branch automatically.

As you are working through your feature continue to work within your local branch and add and commit files as you go along as discussed above.

### Merging feature once complete

This process differs depending on the git host you use. For GitHub, the merge request and code review process is as follows:

First ensure that your final commits have been pushed to your remote branch.

Take these steps to initiate a merge request with code review

1. Navigate to our github repo in a browser
2. Click on the **Pull Request** tab
3. Click on the Green **New pull request button**
4. You will see Two drop-down menus labeled **base** and **compare**, change **base** to Dev and **compare** to the name of your feature branch like the picture [here](https://image.ibb.co/jHLzKc/Screen_Shot_2018_03_14_at_4_05_49_pm.png)
5. Click on the green **Create Pull Request** button
6. Here you can create a title and even leave a message for your merge request. The most important thing, however, is to ensure you select at least one other group member to review your code under the **Reviewers** section on the right of screen.
7. When you are happy with your request title and message and you have **ensured to allocate at least one reviewer**, click on the green **Create Pull Request** button
8. All Commits will now be able to be code reviewed and approved before being merged back into the Dev branch. If there are any issues we can contact each other directly via slack, we can also leave messages on the github page.

### Code Reviewing

When you are assigned to review a pull request you should be notified via the slackbot as well as with an email. If all else fails since we are a team of three we can make the request directly via slack if necessary.

All that is required of the reviewer is to ensure the code is correct and at an acceptable standard, The requests can be found beneath the **Pull Requests** tab on the github homepage. If the reviewer is happy with the changes, simply click the green **Merge Pull Request** button and the changes will be Merged into Dev.

I would recommend deleting the feature branch once the merge has been completed although this is not essential.


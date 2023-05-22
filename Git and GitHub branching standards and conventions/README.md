## Contents

- [Branching](#branching)
- [Quick Legend](#quick-legend)
- [Main Branches](#main-branches)
- [Supporting Branches](#supporting-branches)
  - [Feature Branches](#feature-branches)
    - [Working with a feature branch](#working-with-a-feature-branch)
  - [Bug Branches](#bug-branches)
    - [Working with a bug branch](#working-with-a-bug-branch)
  - [Hotfix Branches](#hotfix-branches)
    - [Working with a hotfix branch](#working-with-a-hotfix-branch)

# Branching

## Quick Legend

<table>
  <thead>
    <tr>
      <th>Instance</th>
      <th>Branch</th>
      <th>Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>Stable</td>
      <td>stable</td>
      <td>Accepts merges from Working and Hotfixes</td>
    </tr>
    <tr>
      <td>Working</td>
      <td>master</td>
      <td>Accepts merges from Features/Bugs and Hotfixes</td>
    </tr>
    <tr>
      <td>Features/Bugs</td>
      <td>feature-*/bug-*</td>
      <td>Always branch off HEAD of Working</td>
    </tr>
    <tr>
      <td>Hotfix</td>
      <td>hotfix-*</td>
      <td>Always branch off Stable</td>
    </tr>
  </tbody>
</table>

## Main Branches

The primary repository will maintain two perpetual branches:

* `master`
* `stable`

The `master` branch should be regarded as `origin/master` and will serve as the primary branch
where the source code of `HEAD` consistently reflects the most recent development changes 
for the next release. As a developer, you will create branches and merge from `master`.

The `stable` branch, on the other hand, must always indicate the latest code that has been deployed in production. 
During daily development, the stable branch will not be interacted with.

## Supporting Branches

To facilitate parallel development among team members, track features easily, 
and promptly resolve live production issues, we use supporting branches. 
These branches, unlike the primary branches, have a restricted lifespan and will eventually be removed.

We use the following types of branches:

* Feature branches
* Bug branches
* Hotfix branches

Each of these branches serves a unique purpose and must adhere to stringent regulations regarding originating and merging target branches. Below, we'll explain each branch and its intended function.

### Feature Branches

When developing a new feature or enhancement with a potential development lifespan longer than a single deployment,
feature branches are utilized. 
It's possible that the deployment for releasing this feature is not immediately known at the start of development. 
The feature branch will always be merged back into the master branch regardless of when it's completed.

Throughout the feature development, the lead should monitor the `master` branch 
(using a network tool or branch tool in GitHub) for any commits made since branching. 
All changes to the `master` should be merged into the feature before merging back to the `master` branch. 
This can be done at any point during the project timeline, but sufficient time should be allocated to handle 
any merge conflicts.

`<tbd number>` represents the project to which Project Management will be tracked.

* Branch must be created from: `master`
* Branch must be merged back into: `master`
* Naming convention: `feature-<tbd number>`

#### Working with a feature branch

Please confirm with the Lead if the branch has not yet been created. 
If it hasn't, create the branch on your local machine and then push it to GitHub. 
It's essential that every feature branch is accessible to everyone involved in the project. 
Development should not be confined to an individual developer's local branch.

```
$ git checkout -b feature-id master                 // We create a local branch for the new feature.
$ git push origin feature-id                        // We make the new feature remotely available.
```

From time to time, changes made to `master` (if any) should be integrated back into your feature branch.

```
$ git merge master                                  // We merge changes from master into feature branch.
```

Once the feature development is finished, it's the responsibility of the lead or engineer
in charge to integrate changes into the `master` and then ensure the remote branch is removed.

```
$ git checkout master                               // We switch to the master branch.
$ git merge --no-ff feature-id                      // We perform a merge while creating a commit object.
$ git push origin master                            // We push the merge changes to the origin.
$ git push origin :feature-id                       // We delete the remote feature branch.
```

### Bug Branches

Bug branches are similar to feature branches in language, but serve a distinct purpose. 
They are created in response to a bug on the live site that needs fixing and should be merged into the next deployment. 
As such, bug branches are usually short-lived and track the difference between bug development and feature development.
Regardless of when the bug branch is completed, it must always be merged back into `master`.

During the bug's lifecycle, the lead should periodically check the `master` branch 
(using a network or branch tool on GitHub) to see if any commits have been made since the branch was created. 
Any changes made to `master` must be merged into the bug before merging back into master. 
This can be done at any time during the project, but time should be allotted to resolve any merge conflicts.

The branch naming convention for bug branches is `bug-<tbd number>`, 
which represents the Basecamp project where Project Management will be tracked.

* Must branch from: `master`
* Must merge back into: `master`
* Branch naming convention: `bug-<tbd number>`

#### Working with a bug branch

Ensure the branch doesn't already exist (verify with the Lead) before creating it locally and pushing it to GitHub. 
A bug branch should always be 'publicly' available. 
That is, development should never exist in just one developer's local branch.

```
$ git checkout -b bug-id master                     // We create a local branch for the new bug.
$ git push origin bug-id                            // We push the new bug and make it available for others.
```

Periodically, any changes made to `master` (if at all) must be incorporated into your bug branch.

```
$ git merge master                                   // We merge changes from master into the bug branch.
```

Upon completing development work on the bug, Lead should merge the changes with `master` and
guarantee removal of the remote branch.

```
$ git checkout master                               // We switch to the master branch.
$ git merge --no-ff bug-id                          // We enforces the creation of a commit object during merge.
$ git push origin master                            // We push merge to the origin.
$ git push origin :bug-id                           // We delete the branch.
```

### Hotfix Branches

A hotfix branch is created to quickly address an undesired state of a live production version. 
It does not need to be pushed during a scheduled deployment due to its urgency. 
To meet these requirements, a hotfix branch is always branched from a tagged `stable` branch. 
This is necessary for two reasons:

* Development on the `master` branch can continue while the hotfix is being resolved.
* A tagged `stable` branch reflects what is currently in production. 
At the time a hotfix is necessary, there may have been several commits to the `master` branch
that no longer correspond to the production version.

`<tbd number>` will serve as the Basecamp project for tracking Project Management.

* Branch from: tagged `stable`
* Merge back into: `master` and `stable`
* Naming convention for branch: `hotfix-<tbd number>`

#### Working with a hotfix branch

If the branch doesn't already exist (check with the Lead), generate the branch locally and then push to GitHub.
A hotfix branch should always be 'publicly' accessible, 
meaning that development should never exist in only one developer's local branch.

```
$ git checkout -b hotfix-id stable                  // We create a local branch for the new hotfix.
$ git push origin hotfix-id                         // We make the new hotfix remotely accessible.
```

After development on the hotfix wraps up, Lead should merge changes into `stable` and then update the tag.

```
$ git checkout stable                               // We switch to the stable branch.
$ git merge --no-ff hotfix-id                       // We force creation of commit object during merge.
$ git tag -a <tag>                                  // We tag the fix.
$ git push origin stable --tags                     // We push tag changes.
```

Merge changes into `master` to preserve the hotfix, and then delete the remote hotfix branch.

```
$ git checkout master                               // We switch to the master.
$ git merge --no-ff hotfix-id                       // We forces creation of commit object during merge.
$ git push origin master                            // We push merge changes.
$ git push origin :hotfix-id                        // We romoves the remote branch.
```
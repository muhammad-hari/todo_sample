namespace Todoist.Domain.DataTypes
{
    public enum TaskStatusTypes
    {
        /// <summary>
        /// Tasks that have been identified and added to the task list but have not been started yet. These tasks are awaiting action.
        /// </summary>
        ToDo = 0,

        /// <summary>
        /// Tasks that have been started but are not yet completed. Work is actively being done to move the task toward completion.
        /// </summary>
        InProgress,

        /// <summary>
        /// Tasks that are on hold or waiting for something external to happen before they can progress. They may be waiting for approval, information, or resources.
        /// </summary>
        Pending,

        /// <summary>
        /// Tasks that cannot progress due to some impediment or dependency. They are waiting for a resolution to the blocking issue before they can proceed.
        /// </summary>
        OnHold,

        /// <summary>
        /// Tasks that have been finished successfully and have met their objectives. No further action is required for these tasks.
        /// </summary>
        Completed



    }
}

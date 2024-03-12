namespace Todoist.Domain.DataTypes
{
    public enum TaskPriorityTypes
    {
        /// <summary>
        /// Tasks that do not have a defined priority level or whose priority is not yet determined. This category may include tasks that are newly created or still being evaluated.
        /// </summary>
        None = 0,

        /// <summary>
        /// Tasks that are of utmost importance and have severe consequences if not completed on time. They often involve emergencies or situations where failure is not an option.
        /// </summary>
        Critical,

        /// <summary>
        /// Tasks that require prompt action and should be completed as soon as possible. These tasks are critical for achieving goals or meeting deadlines.
        /// </summary>
        High,

        /// <summary>
        ///  Tasks that need attention but are not critical for immediate action. They are important but can be managed within a reasonable timeframe.
        /// </summary>
        Medium,

        /// <summary>
        /// Tasks that are not urgent and can be postponed if necessary. These tasks typically have a lower impact on overall goals and deadlines.
        /// </summary>
        Low
    }
}

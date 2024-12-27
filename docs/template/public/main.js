import WorkflowContainer from "./workflow.js"

export default {
    defaultTheme: 'light',
    iconLinks: [{
        icon: 'github',
        href: 'https://github.com/bonvision/BonVision',
        title: 'GitHub'
    }],
    start: () => {
        WorkflowContainer.init();
    }
}
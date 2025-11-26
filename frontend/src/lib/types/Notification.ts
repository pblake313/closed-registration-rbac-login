export interface Notification {
    title: string,
    message: string,
    type: 'error' | 'neutral' | 'warn' | 'success'
}
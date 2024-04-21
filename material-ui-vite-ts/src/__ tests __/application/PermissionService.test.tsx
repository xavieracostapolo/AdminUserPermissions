import '@testing-library/jest-dom'
import { renderHook, waitFor } from '@testing-library/react'
import { useCreatePermission, useGetPermission } from '../../application'
import { Permission } from '../../domain'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'

test('demo', () => {
    expect(true).toBe(true)
})

describe('Test application service', () => {
    test('When useGetPermission exec return OK', async () => {
        const queryClient = new QueryClient()
        const wrapper = ({ children }: any) => (
            <QueryClientProvider client={queryClient}>
                {children}
            </QueryClientProvider>
        )

        const { result } = renderHook(() => useGetPermission(), { wrapper })

        await waitFor(() => expect(result.current.isSuccess).toBe(true))        
    })
})

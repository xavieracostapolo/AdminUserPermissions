import '@testing-library/jest-dom'
import { FormDetail, FormSave } from '../../presentation/components'
import { render, screen } from '@testing-library/react'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { Permission } from '../../domain'
import LinearProgress from '@mui/material/LinearProgress'

describe('Render form detail component', () => {
    test('Render component', () => {
        render(
            <QueryClientProvider client={new QueryClient()}>
                <FormDetail />
            </QueryClientProvider>
        )
        expect(screen.getByText('Detail Permission')).toBeInTheDocument()
        expect(true).toBeTruthy()
    })

    test('Render Linear progess component', () => {
        const permissions: Permission[] = [
            {
                nombreEmpleado: 'nombre1',
                apellidoEmpleado: 'apellido1',
                tipoPermiso: 'tipopermiso1',
            },
            {
                nombreEmpleado: 'nombre2',
                apellidoEmpleado: 'apellido2',
                tipoPermiso: 'tipopermiso2',
            },
        ]

        jest.mock('../../application/usePermissionService', () => ({
            useGetPermission: jest.fn(() => ({
                isLoading: true,
                data: permissions,
            })),
        }))

        render(<LinearProgress />)
    })
})

describe('Render form save component', () => {
    test('Render component', () => {
        render(
            <QueryClientProvider client={new QueryClient()}>
                <FormSave />
            </QueryClientProvider>
        )
        expect(screen.getByText('Create Permission')).toBeInTheDocument()
        expect(true).toBeTruthy()
    })
})

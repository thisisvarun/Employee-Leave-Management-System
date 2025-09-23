import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageTeams } from './manage-teams';

describe('ManageTeams', () => {
  let component: ManageTeams;
  let fixture: ComponentFixture<ManageTeams>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManageTeams]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageTeams);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
